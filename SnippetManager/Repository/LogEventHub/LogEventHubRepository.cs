using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using SnippetManager.Models.LogEventHub;

namespace SnippetManager.Repository.LogEventHub
{
    public class LogEventHubRepository : ILogEventFactoryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly EventHubBufferedProducerClient _bufferedProducerClient;

        public LogEventHubRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _bufferedProducerClient = new EventHubBufferedProducerClient(
                configuration["ConnectionStrings:EventHub"],
                new EventHubBufferedProducerClientOptions()
                {
                    MaximumWaitTime = TimeSpan.FromSeconds(1),
                    MaximumEventBufferLengthPerPartition = 1500
                });

            _bufferedProducerClient.SendEventBatchFailedAsync += _SendEventBatchFailedAsync;
        }

        private async Task _SendEventBatchFailedAsync(SendEventBatchFailedEventArgs arg)
        {
            throw new NotImplementedException();
        }

        public async Task EnqueAsync(String email)
        {
            //string serializedLogEvent = JsonSerializer.Serialize(logEvent, new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //});

            await _bufferedProducerClient.EnqueueEventAsync(new EventData(Encoding.UTF8.GetBytes(email)));
        }

        public async Task DisposeAsync()
        {
            await _bufferedProducerClient.CloseAsync(flush: true);
        }
    }
}

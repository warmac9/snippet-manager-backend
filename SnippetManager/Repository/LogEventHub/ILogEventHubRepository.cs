using SnippetManager.Models.LogEventHub;

namespace SnippetManager.Repository.LogEventHub
{
    public interface ILogEventFactoryRepository
    {
        Task DisposeAsync();

        Task EnqueAsync(String email);
    }
}
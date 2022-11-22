namespace SnippetManager.Models.LogEventHub;

public record UserData
{
    public string UserName { get; init; }

    public string IpAddress { get; init; }
}
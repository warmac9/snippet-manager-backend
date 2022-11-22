namespace SnippetManager.Models.LogEventHub;

public record LogEvent
{
    public String OperationName { get; init; }

    public UserData UserData { get; init; }
}
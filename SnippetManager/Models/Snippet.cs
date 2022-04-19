namespace SnippetManager.Models;

public record Snippet
{
    public int Id { get; init; }

    public string UserId { get; init; }

    public string Title { get; init; }
    
    public string Content { get; init; }
}
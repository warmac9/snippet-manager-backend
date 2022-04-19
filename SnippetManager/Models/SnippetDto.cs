using System.ComponentModel.DataAnnotations;

namespace SnippetManager.Models;

public record SnippetDto
{
    public int Id { get; init; }

    [Required]
    public string Title { get; init; }

    [Required]
    public string Content { get; init; }
}
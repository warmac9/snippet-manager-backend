using System.ComponentModel.DataAnnotations;

namespace SnippetManager.Models;

public class SnippetDto
{
    public int ID { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }
}
using Microsoft.EntityFrameworkCore;
using SnippetManager.Models;

namespace SnippetManager.Database;

public class SnippetManagerContext : DbContext
{
    public SnippetManagerContext(DbContextOptions<SnippetManagerContext> options)
        : base(options)
    {
    }
    
    public DbSet<Snippet> Snippet { get; set; }
}
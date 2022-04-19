using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnippetManager.Models;

namespace SnippetManager.Database;

public class SnippetManagerContext : IdentityDbContext<AppUser>
{
    public SnippetManagerContext(DbContextOptions<SnippetManagerContext> options)
        : base(options)
    {
    }
    
    public DbSet<Snippet> Snippet { get; set; }
}
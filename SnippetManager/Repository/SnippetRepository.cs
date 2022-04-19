using System.Dynamic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using SnippetManager.Database;
using SnippetManager.Helpers;
using SnippetManager.Models;

namespace SnippetManager.Repository;

public class SnippetRepository : ISnippetRepository
{
    private readonly SnippetManagerContext _context;
    private readonly IMapper _mapper;
    private readonly AppUser? _curUser;

    public SnippetRepository(SnippetManagerContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager)
    {
        _context = context;
        _mapper = mapper;

        var curUserEmail = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        if (curUserEmail != null)
        {
            _curUser = userManager.FindByEmailAsync(curUserEmail).Result;
        }
    }

    public async Task<List<SnippetDto>> GetAllAsync()
    {
        //var records = await _context.Snippet.Select(x => new SnippetDto()
        //{
        //    ID = x.ID,
        //    Title = x.Title,
        //    Content = x.Content
        //}).ToListAsync();
        //return records;

        var records = await _context.Snippet.Where(x => _curUser.Id == x.UserId).ToListAsync();
        return _mapper.Map<List<SnippetDto>>(records);
    }

    public async Task<SnippetDto?> GetByIdAsync(int id)
    {
        var record = await _context.Snippet.Where(x => _curUser.Id == x.UserId && x.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<SnippetDto>(record);
    }

    public async Task<int> AddAsync(SnippetDto model)
    {
        var snippet = new Snippet()
        {
            UserId = _curUser.Id,
            Title = model.Title,
            Content = model.Content
        };

        _context.Snippet.Add(snippet);
        await _context.SaveChangesAsync();

        return snippet.Id;
    }

    public async Task<Snippet?> UpdateAsync(int id, SnippetDto model)
    {
        //unnecessary two database operations
        //var snippet = _context.Snippet.FindAsync(snippetId);
        //if (snippet != null) {
        //    snippet.Result.Title = snippetModel.Title;
        //    snippet.Result.Content = snippetModel.Content;
        //    snippet.Result.UnlockDate = snippetModel.UnlockDate;

        //    await _context.SaveChangesAsync();
        //}

        var snippet = new Snippet()
        {
            Id = id,
            UserId = _curUser.Id,
            Title = model.Title,
            Content = model.Content
        };
        _context.Snippet.Update(snippet);

        try
        {
            await _context.SaveChangesAsync();
            return snippet;
        }
        //if we update the resource, that already existed, it throws error
        catch (DbUpdateConcurrencyException e)
        {
            return null;
        }
    }

    public async Task<Snippet?> PatchAsync(int id, JsonPatchDocument model)
    {
        var snippet = await _context.Snippet.FindAsync(id);

        if (snippet != null)
        {
            model.ApplyTo(snippet);
            await _context.SaveChangesAsync();
            return snippet;
        }

        return null;
    }

    public async Task<Snippet?> DeleteAsync(int id)
    {
        var snippet = new Snippet()
        {
            Id = id
        };
        _context.Snippet.Remove(snippet);

        try
        {
            await _context.SaveChangesAsync();
            return snippet;
        }
        //if we delete the resource, that does not exists, it throws error
        catch (DbUpdateConcurrencyException e)
        {
            return null;
        }
    }
}
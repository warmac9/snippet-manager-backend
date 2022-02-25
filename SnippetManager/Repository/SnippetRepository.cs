using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using SnippetManager.Database;
using SnippetManager.Models;

namespace SnippetManager.Repository;

public class SnippetRepository : ISnippetRepository
{
    private readonly SnippetManagerContext _context;
    private readonly IMapper _mapper;

    public SnippetRepository(SnippetManagerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

        var records = await _context.Snippet.ToListAsync();
        return _mapper.Map<List<SnippetDto>>(records);
    }

    public async Task<SnippetDto?> GetByIdAsync(int id)
    {
        var record = await _context.Snippet.Where(x => x.ID == id).FirstOrDefaultAsync();
        return _mapper.Map<SnippetDto>(record);
    }

    public async Task<int> AddAsync(SnippetDto model)
    {
        var snippet = new Snippet()
        {
            Title = model.Title,
            Content = model.Content
        };

        _context.Snippet.Add(snippet);
        await _context.SaveChangesAsync();

        return snippet.ID;
    }

    public async Task<Snippet?> UpdateAsync(int id, SnippetDto model)
    {
        //unneeccesary two database operations
        //var snippet = _context.Snippet.FindAsync(snippetId);
        //if (snippet != null) {
        //    snippet.Result.Title = snippetModel.Title;
        //    snippet.Result.Content = snippetModel.Content;
        //    snippet.Result.UnlockDate = snippetModel.UnlockDate;

        //    await _context.SaveChangesAsync();
        //}

        var snippet = new Snippet()
        {
            ID = id,
            Title = model.Title,
            Content = model.Content
        };
        _context.Snippet.Update(snippet);

        try
        {
            await _context.SaveChangesAsync();
            return snippet;
        }
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
            ID = id
        };
        _context.Snippet.Remove(snippet);

        try
        {
            await _context.SaveChangesAsync();
            return snippet;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return null;
        }
    }
}
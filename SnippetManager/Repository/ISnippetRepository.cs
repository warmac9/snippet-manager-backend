using SnippetManager.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;
using SnippetManager.Helpers;

namespace SnippetManager.Repository
{
    public interface ISnippetRepository
    {
        Task<List<SnippetDto>> GetAllAsync();

        Task<SnippetDto?> GetByIdAsync(int id);

        Task<int> AddAsync(SnippetDto model);

        Task<Snippet?> UpdateAsync(int id, SnippetDto model);

        Task<Snippet?> PatchAsync(int id, JsonPatchDocument model);

        Task<Snippet?> DeleteAsync (int id);
    }
}
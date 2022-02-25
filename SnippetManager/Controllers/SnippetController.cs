using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SnippetManager.Models;
using SnippetManager.Repository;

namespace SnippetManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SnippetController : ControllerBase
{
    private readonly ISnippetRepository _snippetRepository;

    public SnippetController(ISnippetRepository snippetRepository)
    {
        _snippetRepository = snippetRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var records = await _snippetRepository.GetAllAsync();
        return Ok(records);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var record = await _snippetRepository.GetByIdAsync(id);
        if (record == null) return NotFound();
        return Ok(record);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SnippetDto model)
    {
        var id = await _snippetRepository.AddAsync(model);
        return CreatedAtAction(nameof(Add), id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SnippetDto model)
    {
        var res = await _snippetRepository.UpdateAsync(id, model);
        if (res == null) return NotFound();
        return Ok();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument model)
    {
        var res = await _snippetRepository.PatchAsync(id, model);
        if (res == null) return NotFound();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _snippetRepository.DeleteAsync(id);
        if (res == null) return NotFound();
        return Ok();
    }
}
using Arkive.Application.DTOs;
using Arkive.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arkive.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentosController : ControllerBase
{
    private readonly IDocumentoService _svc;
    public DocumentosController(IDocumentoService svc) => _svc = svc;

    [HttpPost]
    public async Task<ActionResult<DocumentoDto>> Create([FromBody] CreateDocumentoDto dto)
    {
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<DocumentoDto>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        => Ok(await _svc.GetPagedAsync(page, pageSize));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DocumentoDto>> GetById(int id)
    {
        var doc = await _svc.GetByIdAsync(id);
        return doc is null ? NotFound() : Ok(doc);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDocumentoDto dto)
    {
        var updated = await _svc.UpdateAsync(id, dto);
        return updated is null ? NotFound() : NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await _svc.DeleteAsync(id)) ? NoContent() : NotFound();

    [HttpGet("buscar")]
    public async Task<ActionResult<PagedResult<DocumentoDto>>> Buscar(
        [FromQuery] string? autor, [FromQuery] string? tipo, [FromQuery] string? estado,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        => Ok(await _svc.SearchAsync(autor, tipo, estado, page, pageSize));
}

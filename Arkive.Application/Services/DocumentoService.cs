using Arkive.Application.DTOs;
using Arkive.Application.Services;
using Arkive.Application.Abstractions;
using Arkive.Domain.Entities;

namespace Arkive.Application.Services;

public class DocumentoService : IDocumentoService
{
    private readonly IDocumentoRepository _repo;

    public DocumentoService(IDocumentoRepository repo)
    {
        _repo = repo;
    }

    public async Task<DocumentoDto?> GetByIdAsync(int id)
    {
        var doc = await _repo.GetByIdAsync(id);
        return doc is null ? null : new DocumentoDto(doc.Id, doc.Titulo, doc.Autor, doc.Tipo, doc.Estado, doc.FechaRegistro);
    }

    public async Task<PagedResult<DocumentoDto>> GetPagedAsync(int page, int pageSize)
    {
        var result = await _repo.GetPagedAsync(page, pageSize);
        return new PagedResult<DocumentoDto>
        {
            Items = result.Items.Select(d => new DocumentoDto(d.Id, d.Titulo, d.Autor, d.Tipo, d.Estado, d.FechaRegistro)),
            Page = result.Page,
            PageSize = result.PageSize,
            Total = result.Total
        };
    }

    public async Task<DocumentoDto> CreateAsync(CreateDocumentoDto dto)
    {
        var doc = new Documento
        {
            Titulo = dto.Titulo,
            Autor = dto.Autor,
            Tipo = dto.Tipo,
            Estado = dto.Estado,
            FechaRegistro = DateTime.UtcNow
        };

        await _repo.AddAsync(doc);
        return new DocumentoDto(doc.Id, doc.Titulo, doc.Autor, doc.Tipo, doc.Estado, doc.FechaRegistro);
    }

    public async Task<DocumentoDto?> UpdateAsync(int id, UpdateDocumentoDto dto)
    {
        var doc = await _repo.GetByIdAsync(id);
        if (doc == null) return null;

        doc.Titulo = dto.Titulo;
        doc.Autor = dto.Autor;
        doc.Tipo = dto.Tipo;
        doc.Estado = dto.Estado;

        await _repo.UpdateAsync(doc);
        return new DocumentoDto(doc.Id, doc.Titulo, doc.Autor, doc.Tipo, doc.Estado, doc.FechaRegistro);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }

    public async Task<PagedResult<DocumentoDto>> SearchAsync(string? autor, string? tipo, string? estado, int page, int pageSize)
    {
        var result = await _repo.SearchAsync(autor, tipo, estado, page, pageSize);
        return new PagedResult<DocumentoDto>
        {
            Items = result.Items.Select(d => new DocumentoDto(d.Id, d.Titulo, d.Autor, d.Tipo, d.Estado, d.FechaRegistro)),
            Page = result.Page,
            PageSize = result.PageSize,
            Total = result.Total
        };
    }
}

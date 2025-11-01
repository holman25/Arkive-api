using Arkive.Application.Abstractions;
using Arkive.Application.DTOs;
using Arkive.Domain.Entities;
using Arkive.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Arkive.Infrastructure.Repositories;

public class DocumentoRepository : IDocumentoRepository
{
    private readonly AppDbContext _db;
    public DocumentoRepository(AppDbContext db) => _db = db;

    public async Task<Documento?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Documentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<PagedResult<Documento>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var q = _db.Documentos.AsNoTracking().OrderByDescending(x => x.FechaRegistro);
        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return new PagedResult<Documento> { Items = items, Page = page, PageSize = pageSize, Total = total };
    }

    public async Task<PagedResult<Documento>> SearchAsync(string? autor, string? tipo, string? estado, int page, int pageSize, CancellationToken ct = default)
    {
        var q = _db.Documentos.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(autor)) q = q.Where(x => x.Autor.Contains(autor));
        if (!string.IsNullOrWhiteSpace(tipo)) q = q.Where(x => x.Tipo == tipo);
        if (!string.IsNullOrWhiteSpace(estado)) q = q.Where(x => x.Estado == estado);

        q = q.OrderByDescending(x => x.FechaRegistro);
        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return new PagedResult<Documento> { Items = items, Page = page, PageSize = pageSize, Total = total };
    }

    public async Task<Documento> AddAsync(Documento doc, CancellationToken ct = default)
    {
        _db.Documentos.Add(doc);
        await _db.SaveChangesAsync(ct);
        return doc;
    }

    public async Task UpdateAsync(Documento doc, CancellationToken ct = default)
    {
        _db.Documentos.Update(doc);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var doc = await _db.Documentos.FindAsync(new object?[] { id }, ct);
        if (doc is null) return false;
        _db.Documentos.Remove(doc);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}

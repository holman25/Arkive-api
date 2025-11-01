using Arkive.Domain.Entities;
using Arkive.Application.DTOs;

namespace Arkive.Application.Abstractions;

public interface IDocumentoRepository
{
    Task<Documento?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<PagedResult<Documento>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
    Task<PagedResult<Documento>> SearchAsync(string? autor, string? tipo, string? estado, int page, int pageSize, CancellationToken ct = default);
    Task<Documento> AddAsync(Documento doc, CancellationToken ct = default);
    Task UpdateAsync(Documento doc, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);

}

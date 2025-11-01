using Arkive.Application.DTOs;

namespace Arkive.Application.Services;

public interface IDocumentoService
{
    Task<DocumentoDto?> GetByIdAsync(int id);
    Task<PagedResult<DocumentoDto>> GetPagedAsync(int page, int pageSize);
    Task<DocumentoDto> CreateAsync(CreateDocumentoDto dto);
    Task<DocumentoDto?> UpdateAsync(int id, UpdateDocumentoDto dto);
    Task<bool> DeleteAsync(int id);
    Task<PagedResult<DocumentoDto>> SearchAsync(string? autor, string? tipo, string? estado, int page, int pageSize);
}

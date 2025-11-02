using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arkive.Infrastructure.Persistence;

namespace Arkive.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _db;

    public HealthController(AppDbContext db) => _db = db;

    [HttpGet]
    public IActionResult Get() => Ok(new { status = "OK" });

    [HttpGet("archive-check")]
    public async Task<IActionResult> ArchiveCheck()
    {
        var lastLog = await _db.LogsCambiosEstado
            .OrderByDescending(x => x.FechaCambioUtc)
            .Select(x => new {
                x.Id,
                x.DocumentoId,
                x.EstadoAnterior,
                x.EstadoNuevo,
                x.FechaCambioUtc,
                x.Motivo,
                x.UsuarioSistema
            })
            .FirstOrDefaultAsync();

        var desde = DateTime.UtcNow.AddDays(-1);
        var archivados24h = await _db.LogsCambiosEstado
            .CountAsync(x => x.FechaCambioUtc >= desde && x.EstadoNuevo == 3);

        var pendientes90d = await _db.Documentos
            .CountAsync(d => d.Estado == "Pendiente" && d.FechaRegistro <= DateTime.UtcNow.AddDays(-90));

        return Ok(new
        {
            service = "ArchivadorHostedService",
            lastRun = lastLog?.FechaCambioUtc,
            lastEntry = lastLog,
            archivedLast24h = archivados24h,
            pendingOver90d = pendientes90d,
            status = "OK"
        });
    }
}

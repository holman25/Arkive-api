using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Arkive.Infrastructure.Persistence;
using Arkive.Domain.Entities;
using Arkive.Infrastructure.Config;

namespace Arkive.Infrastructure.Jobs
{
    public class ArchivadorHostedService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<ArchivadorHostedService> _logger;
        private readonly ArchivadorSettings _settings;

        public ArchivadorHostedService(
            IServiceProvider services,
            ILogger<ArchivadorHostedService> logger,
            IOptions<ArchivadorSettings> settings)
        {
            _services = services;
            _logger = logger;
            _settings = settings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_settings.Enabled)
            {
                _logger.LogInformation("Archivador deshabilitado en configuración.");
                return;
            }

            _logger.LogInformation("Archivador iniciado. Intervalo: {Interval}h", _settings.IntervalHours);

            while (!stoppingToken.IsCancellationRequested)
            {
                try { await RunOnce(stoppingToken); }
                catch (Exception ex) { _logger.LogError(ex, "Error en ArchivadorHostedService"); }

                if (!string.IsNullOrWhiteSpace(_settings.Hora))
                {
                    var now = DateTime.Now;
                    var horaParts = _settings.Hora.Split(':');
                    var hora = new TimeSpan(int.Parse(horaParts[0]), int.Parse(horaParts[1]), 0);
                    var proxima = now.Date.AddDays(now.TimeOfDay > hora ? 1 : 0).Add(hora);
                    var espera = proxima - now;

                    _logger.LogInformation("Próxima ejecución programada para {Proxima}", proxima);
                    await Task.Delay(espera, stoppingToken);
                }
                else
                {
                    await Task.Delay(TimeSpan.FromHours(_settings.IntervalHours), stoppingToken);
                }
            }
        }

        private async Task RunOnce(CancellationToken ct)
        {
            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var limite = DateTime.UtcNow.AddDays(-90);
            var candidatos = await db.Documentos
                .Where(d => d.Estado == "Pendiente" && d.FechaRegistro <= limite)
                .ToListAsync(ct);

            if (candidatos.Count == 0)
            {
                _logger.LogInformation("Archivador: 0 documentos para archivar.");
                return;
            }

            foreach (var d in candidatos)
            {
                var estadoAnteriorStr = d.Estado;
                d.Estado = "Archivado";

                db.LogsCambiosEstado.Add(new LogCambioEstado
                {
                    DocumentoId = d.Id,
                    EstadoAnterior = MapEstadoToCode(estadoAnteriorStr),
                    EstadoNuevo = 3,
                    FechaCambioUtc = DateTime.UtcNow,
                    Motivo = "Archivado por antigüedad > 90 días",
                    UsuarioSistema = "HostedService"
                });
            }

            await db.SaveChangesAsync(ct);
            _logger.LogInformation("Archivador: {Count} documento(s) archivado(s).", candidatos.Count);
        }

        private static int MapEstadoToCode(string? estado)
        {
            if (string.IsNullOrWhiteSpace(estado)) return -1;
            if (estado.Equals("Registrado", StringComparison.OrdinalIgnoreCase)) return 0;
            if (estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase)) return 1;
            if (estado.Equals("Validado", StringComparison.OrdinalIgnoreCase)) return 2;
            if (estado.Equals("Archivado", StringComparison.OrdinalIgnoreCase)) return 3;
            return -1;
        }
    }
}

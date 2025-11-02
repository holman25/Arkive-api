using Arkive.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arkive.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Documento> Documentos => Set<Documento>();
        public DbSet<LogCambioEstado> LogsCambiosEstado => Set<LogCambioEstado>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LogCambioEstado>(b =>
            {
                b.ToTable("LogsCambiosEstado");
                b.HasKey(x => x.Id);
                b.Property(x => x.Motivo).HasMaxLength(500);
                b.Property(x => x.UsuarioSistema).HasMaxLength(100);
                b.HasIndex(x => x.DocumentoId);
                b.HasIndex(x => x.FechaCambioUtc);
            });
        }
    }
}

using Arkive.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arkive.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Documento> Documentos => Set<Documento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Documento>(e =>
        {
            e.ToTable("Documentos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Titulo).HasMaxLength(200).IsRequired();
            e.Property(x => x.Autor).HasMaxLength(150).IsRequired();
            e.Property(x => x.Tipo).HasMaxLength(50).IsRequired();
            e.Property(x => x.Estado).HasMaxLength(20).IsRequired();
            e.Property(x => x.FechaRegistro).HasColumnType("datetime2");
        });
    }
}

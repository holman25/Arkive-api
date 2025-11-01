namespace Arkive.Domain.Entities;

public class Documento
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Autor { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public string Estado { get; set; } = "Registrado";
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
}

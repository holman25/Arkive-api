using System;

namespace Arkive.Domain.Entities
{
    public class LogCambioEstado
    {
        public int Id { get; set; }
        public int DocumentoId { get; set; }

        public int EstadoAnterior { get; set; }
        public int EstadoNuevo { get; set; }

        public DateTime FechaCambioUtc { get; set; }
        public string? Motivo { get; set; }
        public string? UsuarioSistema { get; set; }
    }
}

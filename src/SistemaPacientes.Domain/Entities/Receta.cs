using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class Receta : BaseEntity
{
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string? Notas { get; set; }

    public int ConsultaId { get; set; }
    public Consulta? Consulta { get; set; }

    public ICollection<RecetaDetalle> Detalles { get; set; } = new List<RecetaDetalle>();
}

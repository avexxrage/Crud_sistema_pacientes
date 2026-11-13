using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class Consulta : BaseEntity
{
    public DateTime FechaConsulta { get; set; } = DateTime.UtcNow;
    public string MotivoConsulta { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
    public decimal? Peso { get; set; }
    public decimal? Altura { get; set; }

    public int CitaId { get; set; }
    public Cita? Cita { get; set; }

    public ICollection<SignoVital> SignosVitales { get; set; } = new List<SignoVital>();
    public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();
    public Receta? Receta { get; set; }
}

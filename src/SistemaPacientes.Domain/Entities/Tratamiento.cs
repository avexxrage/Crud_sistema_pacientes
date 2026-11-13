using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class Tratamiento : BaseEntity
{
    public string Descripcion { get; set; } = string.Empty;
    public string? Indicaciones { get; set; }
    public int DuracionDias { get; set; }

    public int DiagnosticoId { get; set; }
    public Diagnostico? Diagnostico { get; set; }
}

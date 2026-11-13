using SistemaPacientes.Domain.Common;
using SistemaPacientes.Domain.Enums;

namespace SistemaPacientes.Domain.Entities;

public class Diagnostico : BaseEntity
{
    public string CodigoCie10 { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public TipoDiagnostico Tipo { get; set; } = TipoDiagnostico.Presuntivo;

    public int ConsultaId { get; set; }
    public Consulta? Consulta { get; set; }

    public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}

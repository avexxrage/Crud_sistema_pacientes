using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class Medicamento : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? PrincipioActivo { get; set; }
    public string? Presentacion { get; set; }
    public string? Concentracion { get; set; }
    public int Stock { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<RecetaDetalle> RecetaDetalles { get; set; } = new List<RecetaDetalle>();
}

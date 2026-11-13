using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class Consultorio : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int Piso { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}

using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class Especialidad : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;

    // Relación 1..* : una especialidad tiene muchos médicos
    public ICollection<Medico> Medicos { get; set; } = new List<Medico>();
}

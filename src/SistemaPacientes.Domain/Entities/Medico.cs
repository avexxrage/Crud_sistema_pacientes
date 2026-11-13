using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class Medico : BaseEntity
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string CedulaProfesional { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public bool Activo { get; set; } = true;

    public int EspecialidadId { get; set; }
    public Especialidad? Especialidad { get; set; }

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}

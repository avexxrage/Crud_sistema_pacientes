namespace SistemaPacientes.Application.DTOs;
public class MedicoDto
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string CedulaProfesional { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public bool Activo { get; set; }
    public int EspecialidadId { get; set; }
    public string? EspecialidadNombre { get; set; }
}

using SistemaPacientes.Domain.Enums;
namespace SistemaPacientes.Application.DTOs;
public class PacienteDto
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public Genero Genero { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public string? TipoSangre { get; set; }
    public bool Activo { get; set; }
}

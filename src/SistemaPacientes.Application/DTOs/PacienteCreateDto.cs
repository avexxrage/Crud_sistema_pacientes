using System.ComponentModel.DataAnnotations;
using SistemaPacientes.Domain.Enums;
namespace SistemaPacientes.Application.DTOs;
public class PacienteCreateDto
{
    [Required, StringLength(80)] public string Nombres { get; set; } = string.Empty;
    [Required, StringLength(80)] public string Apellidos { get; set; } = string.Empty;
    [Required, StringLength(20)] public string Cedula { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public Genero Genero { get; set; }
    [StringLength(20)] public string? Telefono { get; set; }
    [EmailAddress] public string? Email { get; set; }
    [StringLength(200)] public string? Direccion { get; set; }
    [StringLength(5)] public string? TipoSangre { get; set; }
    public bool Activo { get; set; } = true;
}

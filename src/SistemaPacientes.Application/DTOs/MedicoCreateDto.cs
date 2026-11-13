using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class MedicoCreateDto
{
    [Required, StringLength(80)] public string Nombres { get; set; } = string.Empty;
    [Required, StringLength(80)] public string Apellidos { get; set; } = string.Empty;
    [Required, StringLength(30)] public string CedulaProfesional { get; set; } = string.Empty;
    [StringLength(20)] public string? Telefono { get; set; }
    [EmailAddress] public string? Email { get; set; }
    public bool Activo { get; set; } = true;
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especialidad.")]
    public int EspecialidadId { get; set; }
}

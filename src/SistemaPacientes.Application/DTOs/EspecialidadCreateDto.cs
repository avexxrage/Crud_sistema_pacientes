using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class EspecialidadCreateDto
{
    [Required, StringLength(100)] public string Nombre { get; set; } = string.Empty;
    [StringLength(300)] public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;
}

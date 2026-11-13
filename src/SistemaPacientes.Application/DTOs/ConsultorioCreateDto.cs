using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class ConsultorioCreateDto
{
    [Required, StringLength(20)] public string Numero { get; set; } = string.Empty;
    [Range(0, 50)] public int Piso { get; set; }
    [StringLength(300)] public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;
}

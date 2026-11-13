using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class RecetaCreateDto
{
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    [StringLength(500)] public string? Notas { get; set; }
    [Range(1, int.MaxValue)] public int ConsultaId { get; set; }
}

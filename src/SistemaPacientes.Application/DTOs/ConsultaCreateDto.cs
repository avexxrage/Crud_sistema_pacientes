using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class ConsultaCreateDto
{
    public DateTime FechaConsulta { get; set; } = DateTime.UtcNow;
    [Required, StringLength(250)] public string MotivoConsulta { get; set; } = string.Empty;
    [StringLength(1000)] public string? Observaciones { get; set; }
    public decimal? Peso { get; set; }
    public decimal? Altura { get; set; }
    [Range(1, int.MaxValue)] public int CitaId { get; set; }
}

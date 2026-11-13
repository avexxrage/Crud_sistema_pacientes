using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class RecetaDetalleCreateDto
{
    [Required, StringLength(60)] public string Dosis { get; set; } = string.Empty;
    [Required, StringLength(60)] public string Frecuencia { get; set; } = string.Empty;
    [Required, StringLength(60)] public string Duracion { get; set; } = string.Empty;
    [Range(1, int.MaxValue)] public int Cantidad { get; set; }
    [Range(1, int.MaxValue)] public int RecetaId { get; set; }
    [Range(1, int.MaxValue)] public int MedicamentoId { get; set; }
}

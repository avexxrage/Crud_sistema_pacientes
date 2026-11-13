using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class TratamientoCreateDto
{
    [Required, StringLength(300)] public string Descripcion { get; set; } = string.Empty;
    [StringLength(500)] public string? Indicaciones { get; set; }
    [Range(1, 365)] public int DuracionDias { get; set; }
    [Range(1, int.MaxValue)] public int DiagnosticoId { get; set; }
}

using System.ComponentModel.DataAnnotations;
using SistemaPacientes.Domain.Enums;
namespace SistemaPacientes.Application.DTOs;
public class DiagnosticoCreateDto
{
    [Required, StringLength(15)] public string CodigoCie10 { get; set; } = string.Empty;
    [Required, StringLength(300)] public string Descripcion { get; set; } = string.Empty;
    public TipoDiagnostico Tipo { get; set; } = TipoDiagnostico.Presuntivo;
    [Range(1, int.MaxValue)] public int ConsultaId { get; set; }
}

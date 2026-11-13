using System.ComponentModel.DataAnnotations;
using SistemaPacientes.Domain.Enums;
namespace SistemaPacientes.Application.DTOs;
public class CitaCreateDto
{
    public DateTime FechaHora { get; set; }
    [Required, StringLength(200)] public string Motivo { get; set; } = string.Empty;
    public EstadoCita Estado { get; set; } = EstadoCita.Programada;
    [Range(1, int.MaxValue)] public int PacienteId { get; set; }
    [Range(1, int.MaxValue)] public int MedicoId { get; set; }
    [Range(1, int.MaxValue)] public int ConsultorioId { get; set; }
}

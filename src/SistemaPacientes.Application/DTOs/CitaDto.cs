using SistemaPacientes.Domain.Enums;
namespace SistemaPacientes.Application.DTOs;
public class CitaDto
{
    public int Id { get; set; }
    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public EstadoCita Estado { get; set; }
    public int PacienteId { get; set; }
    public string? PacienteNombre { get; set; }
    public int MedicoId { get; set; }
    public string? MedicoNombre { get; set; }
    public int ConsultorioId { get; set; }
    public string? ConsultorioNumero { get; set; }
}

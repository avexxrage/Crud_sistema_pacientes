namespace SistemaPacientes.Application.DTOs;
public class ConsultaDto
{
    public int Id { get; set; }
    public DateTime FechaConsulta { get; set; }
    public string MotivoConsulta { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
    public decimal? Peso { get; set; }
    public decimal? Altura { get; set; }
    public int CitaId { get; set; }
    public string? PacienteNombre { get; set; }
}

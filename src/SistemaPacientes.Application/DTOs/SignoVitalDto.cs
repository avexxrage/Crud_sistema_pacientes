namespace SistemaPacientes.Application.DTOs;
public class SignoVitalDto
{
    public int Id { get; set; }
    public string? PresionArterial { get; set; }
    public int? FrecuenciaCardiaca { get; set; }
    public decimal? Temperatura { get; set; }
    public int? FrecuenciaRespiratoria { get; set; }
    public int? SaturacionOxigeno { get; set; }
    public int ConsultaId { get; set; }
}

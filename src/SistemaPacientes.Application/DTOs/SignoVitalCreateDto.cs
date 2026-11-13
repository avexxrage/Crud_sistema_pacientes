using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class SignoVitalCreateDto
{
    [StringLength(20)] public string? PresionArterial { get; set; }
    public int? FrecuenciaCardiaca { get; set; }
    public decimal? Temperatura { get; set; }
    public int? FrecuenciaRespiratoria { get; set; }
    public int? SaturacionOxigeno { get; set; }
    [Range(1, int.MaxValue)] public int ConsultaId { get; set; }
}

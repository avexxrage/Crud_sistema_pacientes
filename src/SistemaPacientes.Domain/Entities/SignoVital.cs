using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class SignoVital : BaseEntity
{
    public string? PresionArterial { get; set; }
    public int? FrecuenciaCardiaca { get; set; }
    public decimal? Temperatura { get; set; }
    public int? FrecuenciaRespiratoria { get; set; }
    public int? SaturacionOxigeno { get; set; }

    public int ConsultaId { get; set; }
    public Consulta? Consulta { get; set; }
}

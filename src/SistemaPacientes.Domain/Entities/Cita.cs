using SistemaPacientes.Domain.Common;
using SistemaPacientes.Domain.Enums;

namespace SistemaPacientes.Domain.Entities;

public class Cita : BaseEntity
{
    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public EstadoCita Estado { get; set; } = EstadoCita.Programada;

    public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }

    public int MedicoId { get; set; }
    public Medico? Medico { get; set; }

    public int ConsultorioId { get; set; }
    public Consultorio? Consultorio { get; set; }

    // Relación 1..1 : una cita genera una consulta
    public Consulta? Consulta { get; set; }
}

using SistemaPacientes.Domain.Enums;
namespace SistemaPacientes.Application.DTOs;
public class DiagnosticoDto
{
    public int Id { get; set; }
    public string CodigoCie10 { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public TipoDiagnostico Tipo { get; set; }
    public int ConsultaId { get; set; }
}

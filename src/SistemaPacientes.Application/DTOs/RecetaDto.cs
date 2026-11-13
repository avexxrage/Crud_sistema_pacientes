namespace SistemaPacientes.Application.DTOs;
public class RecetaDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string? Notas { get; set; }
    public int ConsultaId { get; set; }
}

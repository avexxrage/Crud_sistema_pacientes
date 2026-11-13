namespace SistemaPacientes.Application.DTOs;
public class TratamientoDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string? Indicaciones { get; set; }
    public int DuracionDias { get; set; }
    public int DiagnosticoId { get; set; }
}

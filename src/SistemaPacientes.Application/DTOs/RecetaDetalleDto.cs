namespace SistemaPacientes.Application.DTOs;
public class RecetaDetalleDto
{
    public int Id { get; set; }
    public string Dosis { get; set; } = string.Empty;
    public string Frecuencia { get; set; } = string.Empty;
    public string Duracion { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public int RecetaId { get; set; }
    public int MedicamentoId { get; set; }
    public string? MedicamentoNombre { get; set; }
}

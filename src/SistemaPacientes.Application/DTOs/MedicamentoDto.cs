namespace SistemaPacientes.Application.DTOs;
public class MedicamentoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? PrincipioActivo { get; set; }
    public string? Presentacion { get; set; }
    public string? Concentracion { get; set; }
    public int Stock { get; set; }
    public bool Activo { get; set; }
}

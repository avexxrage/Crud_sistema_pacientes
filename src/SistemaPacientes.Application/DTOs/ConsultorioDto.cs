namespace SistemaPacientes.Application.DTOs;
public class ConsultorioDto
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public int Piso { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; }
}

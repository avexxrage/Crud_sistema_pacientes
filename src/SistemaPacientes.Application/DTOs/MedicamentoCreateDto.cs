using System.ComponentModel.DataAnnotations;
namespace SistemaPacientes.Application.DTOs;
public class MedicamentoCreateDto
{
    [Required, StringLength(120)] public string Nombre { get; set; } = string.Empty;
    [StringLength(120)] public string? PrincipioActivo { get; set; }
    [StringLength(60)] public string? Presentacion { get; set; }
    [StringLength(60)] public string? Concentracion { get; set; }
    [Range(0, int.MaxValue)] public int Stock { get; set; }
    public bool Activo { get; set; } = true;
}

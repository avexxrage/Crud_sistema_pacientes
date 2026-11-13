namespace SistemaPacientes.Domain.Common;

/// <summary>
/// Clase base de todas las entidades. Aplica el concepto de herencia y
/// centraliza las columnas de auditoría comunes a toda la capa de dominio.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaModificacion { get; set; }
}

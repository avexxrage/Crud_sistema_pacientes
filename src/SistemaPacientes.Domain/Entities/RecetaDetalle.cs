using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Entities;

public class RecetaDetalle : BaseEntity
{
    public string Dosis { get; set; } = string.Empty;
    public string Frecuencia { get; set; } = string.Empty;
    public string Duracion { get; set; } = string.Empty;
    public int Cantidad { get; set; }

    public int RecetaId { get; set; }
    public Receta? Receta { get; set; }

    public int MedicamentoId { get; set; }
    public Medicamento? Medicamento { get; set; }
}

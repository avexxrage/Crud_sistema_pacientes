namespace SistemaPacientes.Application.Interfaces;

/// <summary>
/// Contrato genérico de la capa de servicios (lógica de negocio).
/// Trabaja con DTOs, nunca con entidades de dominio: así la capa de
/// presentación queda desacoplada del modelo de datos.
/// </summary>
public interface IGenericService<TDto, TCreateDto>
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(int id);
    Task<TDto> CreateAsync(TCreateDto dto);
    Task<TDto?> UpdateAsync(int id, TCreateDto dto);
    Task<bool> DeleteAsync(int id);
}

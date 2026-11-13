using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Interfaces;

/// <summary>
/// Contrato genérico del patrón Repository. La capa de dominio solo conoce
/// la abstracción (inversión de dependencias); la implementación vive en
/// la capa de Infraestructura.
/// </summary>
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    IQueryable<T> Query();
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}

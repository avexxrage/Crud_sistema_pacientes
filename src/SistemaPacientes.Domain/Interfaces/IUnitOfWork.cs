using SistemaPacientes.Domain.Common;

namespace SistemaPacientes.Domain.Interfaces;

/// <summary>
/// Patrón Unit of Work: agrupa los repositorios bajo una misma transacción
/// y expone un único punto para confirmar los cambios.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync();
}

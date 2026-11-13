using SistemaPacientes.Domain.Common;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Persistence;

namespace SistemaPacientes.Infrastructure.Repositories;

/// <summary>
/// Implementación del Unit of Work. Reutiliza una sola instancia de
/// repositorio por tipo y confirma todos los cambios en una transacción.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(ApplicationDbContext context) => _context = context;

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        if (_repositories.TryGetValue(typeof(T), out var existing))
            return (IGenericRepository<T>)existing;

        var repository = new GenericRepository<T>(_context);
        _repositories[typeof(T)] = repository;
        return repository;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}

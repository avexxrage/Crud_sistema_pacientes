using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Common;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Persistence;

namespace SistemaPacientes.Infrastructure.Repositories;

/// <summary>
/// Implementación concreta del patrón Repository sobre EF Core.
/// </summary>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.AsNoTracking().OrderByDescending(e => e.Id).ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public IQueryable<T> Query() => _dbSet.AsQueryable();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Remove(T entity) => _dbSet.Remove(entity);
}

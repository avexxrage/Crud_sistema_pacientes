using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Persistence;
using SistemaPacientes.Infrastructure.Repositories;

namespace SistemaPacientes.Infrastructure;

/// <summary>
/// Registro de dependencias de la capa de Infraestructura
/// (DbContext SQLite, Repository genérico y Unit of Work).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

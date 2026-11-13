using Microsoft.Extensions.DependencyInjection;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Application.Mappings;
using SistemaPacientes.Application.Services;

namespace SistemaPacientes.Application;

/// <summary>
/// Registro de dependencias de la capa de Aplicación (servicios + AutoMapper).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddScoped<IEspecialidadService, EspecialidadService>();
        services.AddScoped<IConsultorioService, ConsultorioService>();
        services.AddScoped<IMedicoService, MedicoService>();
        services.AddScoped<IPacienteService, PacienteService>();
        services.AddScoped<ICitaService, CitaService>();
        services.AddScoped<IConsultaService, ConsultaService>();
        services.AddScoped<ISignoVitalService, SignoVitalService>();
        services.AddScoped<IDiagnosticoService, DiagnosticoService>();
        services.AddScoped<ITratamientoService, TratamientoService>();
        services.AddScoped<IMedicamentoService, MedicamentoService>();
        services.AddScoped<IRecetaService, RecetaService>();
        services.AddScoped<IRecetaDetalleService, RecetaDetalleService>();

        return services;
    }
}

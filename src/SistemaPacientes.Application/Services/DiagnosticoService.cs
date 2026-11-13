using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class DiagnosticoService : GenericService<Diagnostico, DiagnosticoDto, DiagnosticoCreateDto>, IDiagnosticoService
{
    public DiagnosticoService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

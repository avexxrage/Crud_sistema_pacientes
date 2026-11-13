using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class EspecialidadService : GenericService<Especialidad, EspecialidadDto, EspecialidadCreateDto>, IEspecialidadService
{
    public EspecialidadService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

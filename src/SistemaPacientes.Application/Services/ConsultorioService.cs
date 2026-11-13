using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class ConsultorioService : GenericService<Consultorio, ConsultorioDto, ConsultorioCreateDto>, IConsultorioService
{
    public ConsultorioService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

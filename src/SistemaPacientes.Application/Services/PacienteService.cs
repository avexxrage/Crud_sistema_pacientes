using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class PacienteService : GenericService<Paciente, PacienteDto, PacienteCreateDto>, IPacienteService
{
    public PacienteService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class SignoVitalService : GenericService<SignoVital, SignoVitalDto, SignoVitalCreateDto>, ISignoVitalService
{
    public SignoVitalService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class TratamientoService : GenericService<Tratamiento, TratamientoDto, TratamientoCreateDto>, ITratamientoService
{
    public TratamientoService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

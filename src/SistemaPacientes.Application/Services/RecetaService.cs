using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class RecetaService : GenericService<Receta, RecetaDto, RecetaCreateDto>, IRecetaService
{
    public RecetaService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

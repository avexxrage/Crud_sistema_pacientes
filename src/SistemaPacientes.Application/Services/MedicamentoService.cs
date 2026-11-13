using AutoMapper;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class MedicamentoService : GenericService<Medicamento, MedicamentoDto, MedicamentoCreateDto>, IMedicamentoService
{
    public MedicamentoService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}

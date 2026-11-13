using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class ConsultaService : GenericService<Consulta, ConsultaDto, ConsultaCreateDto>, IConsultaService
{
    public ConsultaService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }


    public override async Task<IEnumerable<ConsultaDto>> GetAllAsync()
    {
        var list = await _unitOfWork.Repository<Consulta>().Query().Include(e => e.Cita).ThenInclude(c => c!.Paciente).ToListAsync();
        return _mapper.Map<IEnumerable<ConsultaDto>>(list);
    }

    public override async Task<ConsultaDto?> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Repository<Consulta>().Query().Include(e => e.Cita).ThenInclude(c => c!.Paciente).FirstOrDefaultAsync(e => e.Id == id);
        return entity is null ? null : _mapper.Map<ConsultaDto>(entity);
    }
}

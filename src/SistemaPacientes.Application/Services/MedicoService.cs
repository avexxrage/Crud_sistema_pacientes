using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class MedicoService : GenericService<Medico, MedicoDto, MedicoCreateDto>, IMedicoService
{
    public MedicoService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }


    public override async Task<IEnumerable<MedicoDto>> GetAllAsync()
    {
        var list = await _unitOfWork.Repository<Medico>().Query().Include(e => e.Especialidad).ToListAsync();
        return _mapper.Map<IEnumerable<MedicoDto>>(list);
    }

    public override async Task<MedicoDto?> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Repository<Medico>().Query().Include(e => e.Especialidad).FirstOrDefaultAsync(e => e.Id == id);
        return entity is null ? null : _mapper.Map<MedicoDto>(entity);
    }
}

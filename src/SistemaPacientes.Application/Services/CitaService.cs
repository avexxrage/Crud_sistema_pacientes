using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class CitaService : GenericService<Cita, CitaDto, CitaCreateDto>, ICitaService
{
    public CitaService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }


    public override async Task<IEnumerable<CitaDto>> GetAllAsync()
    {
        var list = await _unitOfWork.Repository<Cita>().Query().Include(e => e.Paciente).Include(e => e.Medico).Include(e => e.Consultorio).ToListAsync();
        return _mapper.Map<IEnumerable<CitaDto>>(list);
    }

    public override async Task<CitaDto?> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Repository<Cita>().Query().Include(e => e.Paciente).Include(e => e.Medico).Include(e => e.Consultorio).FirstOrDefaultAsync(e => e.Id == id);
        return entity is null ? null : _mapper.Map<CitaDto>(entity);
    }
}

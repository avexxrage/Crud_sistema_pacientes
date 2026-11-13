using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public class RecetaDetalleService : GenericService<RecetaDetalle, RecetaDetalleDto, RecetaDetalleCreateDto>, IRecetaDetalleService
{
    public RecetaDetalleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }


    public override async Task<IEnumerable<RecetaDetalleDto>> GetAllAsync()
    {
        var list = await _unitOfWork.Repository<RecetaDetalle>().Query().Include(e => e.Medicamento).ToListAsync();
        return _mapper.Map<IEnumerable<RecetaDetalleDto>>(list);
    }

    public override async Task<RecetaDetalleDto?> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Repository<RecetaDetalle>().Query().Include(e => e.Medicamento).FirstOrDefaultAsync(e => e.Id == id);
        return entity is null ? null : _mapper.Map<RecetaDetalleDto>(entity);
    }
}

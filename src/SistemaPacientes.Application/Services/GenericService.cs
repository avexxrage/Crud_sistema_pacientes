using AutoMapper;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Common;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

/// <summary>
/// Implementación base reutilizable del CRUD. Usa Unit of Work + Repository
/// (inyectados por abstracción) y AutoMapper para el mapeo Entidad &lt;-&gt; DTO.
/// </summary>
public class GenericService<TEntity, TDto, TCreateDto> : IGenericService<TDto, TCreateDto>
    where TEntity : BaseEntity
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await _unitOfWork.Repository<TEntity>().GetAllAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public virtual async Task<TDto?> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
        return entity is null ? default : _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _unitOfWork.Repository<TEntity>().AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto?> UpdateAsync(int id, TCreateDto dto)
    {
        var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
        if (entity is null) return default;

        _mapper.Map(dto, entity);
        entity.FechaModificacion = DateTime.UtcNow;
        _unitOfWork.Repository<TEntity>().Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
        if (entity is null) return false;

        _unitOfWork.Repository<TEntity>().Remove(entity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}

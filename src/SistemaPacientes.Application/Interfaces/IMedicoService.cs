using SistemaPacientes.Application.DTOs;

namespace SistemaPacientes.Application.Interfaces;

public interface IMedicoService : IGenericService<MedicoDto, MedicoCreateDto>
{
}

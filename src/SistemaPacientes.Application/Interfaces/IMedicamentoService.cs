using SistemaPacientes.Application.DTOs;

namespace SistemaPacientes.Application.Interfaces;

public interface IMedicamentoService : IGenericService<MedicamentoDto, MedicamentoCreateDto>
{
}

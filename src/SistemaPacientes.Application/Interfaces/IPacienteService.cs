using SistemaPacientes.Application.DTOs;

namespace SistemaPacientes.Application.Interfaces;

public interface IPacienteService : IGenericService<PacienteDto, PacienteCreateDto>
{
}

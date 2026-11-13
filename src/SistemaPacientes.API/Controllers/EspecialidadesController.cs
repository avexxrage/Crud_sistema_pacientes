using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class EspecialidadesController : GenericController<EspecialidadDto, EspecialidadCreateDto>
{
    public EspecialidadesController(IEspecialidadService service) : base(service) { }
}

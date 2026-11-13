using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class PacientesController : GenericController<PacienteDto, PacienteCreateDto>
{
    public PacientesController(IPacienteService service) : base(service) { }
}

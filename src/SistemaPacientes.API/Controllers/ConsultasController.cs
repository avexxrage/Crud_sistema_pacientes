using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class ConsultasController : GenericController<ConsultaDto, ConsultaCreateDto>
{
    public ConsultasController(IConsultaService service) : base(service) { }
}

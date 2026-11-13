using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class ConsultoriosController : GenericController<ConsultorioDto, ConsultorioCreateDto>
{
    public ConsultoriosController(IConsultorioService service) : base(service) { }
}

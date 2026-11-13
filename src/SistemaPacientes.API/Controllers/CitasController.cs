using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class CitasController : GenericController<CitaDto, CitaCreateDto>
{
    public CitasController(ICitaService service) : base(service) { }
}

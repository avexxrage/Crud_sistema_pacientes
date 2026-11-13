using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class RecetasController : GenericController<RecetaDto, RecetaCreateDto>
{
    public RecetasController(IRecetaService service) : base(service) { }
}

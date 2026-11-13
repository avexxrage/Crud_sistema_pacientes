using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class RecetaDetallesController : GenericController<RecetaDetalleDto, RecetaDetalleCreateDto>
{
    public RecetaDetallesController(IRecetaDetalleService service) : base(service) { }
}

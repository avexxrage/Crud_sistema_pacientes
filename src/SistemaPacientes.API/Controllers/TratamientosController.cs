using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class TratamientosController : GenericController<TratamientoDto, TratamientoCreateDto>
{
    public TratamientosController(ITratamientoService service) : base(service) { }
}

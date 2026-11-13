using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class SignosVitalesController : GenericController<SignoVitalDto, SignoVitalCreateDto>
{
    public SignosVitalesController(ISignoVitalService service) : base(service) { }
}

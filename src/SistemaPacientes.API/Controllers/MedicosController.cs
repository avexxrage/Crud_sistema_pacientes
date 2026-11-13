using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class MedicosController : GenericController<MedicoDto, MedicoCreateDto>
{
    public MedicosController(IMedicoService service) : base(service) { }
}

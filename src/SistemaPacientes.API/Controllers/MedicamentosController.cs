using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class MedicamentosController : GenericController<MedicamentoDto, MedicamentoCreateDto>
{
    public MedicamentosController(IMedicamentoService service) : base(service) { }
}

using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.DTOs;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

public class DiagnosticosController : GenericController<DiagnosticoDto, DiagnosticoCreateDto>
{
    public DiagnosticosController(IDiagnosticoService service) : base(service) { }
}

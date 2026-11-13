using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.API.Controllers;

/// <summary>
/// Controlador base genérico. Expone el CRUD REST completo y es reutilizado
/// por todos los controladores específicos (capa de Presentación / API).
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class GenericController<TDto, TCreateDto> : ControllerBase
{
    protected readonly IGenericService<TDto, TCreateDto> Service;

    protected GenericController(IGenericService<TDto, TCreateDto> service) => Service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TDto>>> GetAll()
        => Ok(await Service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TDto>> GetById(int id)
    {
        var item = await Service.GetByIdAsync(id);
        return item is null ? NotFound(new { mensaje = $"No se encontró el registro {id}." }) : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var creado = await Service.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TDto>> Update(int id, [FromBody] TCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var actualizado = await Service.UpdateAsync(id, dto);
        return actualizado is null ? NotFound(new { mensaje = $"No se encontró el registro {id}." }) : Ok(actualizado);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await Service.DeleteAsync(id);
        return ok ? NoContent() : NotFound(new { mensaje = $"No se encontró el registro {id}." });
    }
}

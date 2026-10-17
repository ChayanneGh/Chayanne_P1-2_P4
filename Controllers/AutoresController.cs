using Chayanne_P1_2_P4.Models;
using Chayanne_P1_2_P4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Chayanne_P1_2_P4.Controllers;

[ApiController]
[Route("api/autores")]
public class AutoresController(AutorService autorService): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> autoresGet()
    {
        try
        {
            var lista = await autorService.GetAsync();
            return Ok(lista);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpGet("{IdAutor}")]
    public async Task<IActionResult> autoresGetId(long IdAutor)
    {
        try
        {
            var autor = await autorService.GetIdAsync(IdAutor);
            return Ok(autor);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> autoresPost([FromBody] Autor autorDto)
    {
        try
        { 
            Autor autor = await autorService.CreateAsync(autorDto);
            return Ok(autor);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPut("{IdAutor}")]
    public async Task<IActionResult> autoresPut(long IdAutor, [FromBody] Autor autorDto)
    {
        try
        {
            if (autorDto.IdAutor != 0 && autorDto.IdAutor != IdAutor)
            {
                return NotFound();
            };
            await autorService.UpdateAsync(autorDto);
            return Ok(new { mensaje = "El registro se a Actualizado" });
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpDelete("{IdAutor}")]
    public async Task<IActionResult> autoresDelete(long IdAutor)
    {
        try
        { 
            await autorService.DeleteAsync(IdAutor);
            return Ok(new { mensaje = "El registro se ha Eliminado"});
        }
        catch
        {
            return NotFound();
        }
    }
}

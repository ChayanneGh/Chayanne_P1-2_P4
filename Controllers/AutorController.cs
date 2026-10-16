using Chayanne_P1_2_P4.Models;
using Chayanne_P1_2_P4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Chayanne_P1_2_P4.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AutorController(AutorService autorService): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> autoresGet()
    {
        var lista = await autorService.GetAsync();
        return Ok(lista);
    }

    [HttpGet("{IdAutor}")]
    public async Task<IActionResult> autoresGetId(int IdAutor)
    {
        Autor autor = await autorService.GetIdAsync(IdAutor);
        return Ok(autor);
    }

    [HttpPost]
    public async Task<IActionResult> autoresPost([FromBody] Autor autorDto)
    {
        Autor autor = await autorService.CreateAsync(autorDto);
        return Ok(autor);
    }

    [HttpPut("{IdAutor}")]
    public async Task<IActionResult> autoresPut(int IdAutor, [FromBody] Autor autorDto)
    {
        if (autorDto.IdAutor != 0 && autorDto.IdAutor != IdAutor)
        {
            return BadRequest($"Los IDs no coinciden, ID:{IdAutor} != IdAutor:{autorDto.IdAutor}");
        };
        await autorService.UpdateAsync(autorDto);
        return Ok(new { mensaje = "El registro se a Actualizado"});
    }

    [HttpDelete("{IdAutor}")]
    public async Task<IActionResult> autoresDelete(int IdAutor)
    {
        await autorService.DeleteAsync(IdAutor);
        return Ok(new { mensaje = "El registro se ha Eliminado"});
    }
}

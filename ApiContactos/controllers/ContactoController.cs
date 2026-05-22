using Microsoft.AspNetCore.Mvc;
using ApiContactos.path;
namespace AO1_PROG_MOVIL_3.controllers;

[ApiController]
[Route("api/contacto")]

public class ContactoController : ControllerBase
{
    private readonly ContactoService contactoService;

    public ContactoController(ContactoService contactoService)
    {
        this.contactoService = contactoService;
    }

    [HttpGet]
    public ActionResult<List<Contacto>> GetAll()
    {
        return contactoService.ObtenerTodos();
    }

    [HttpGet("{id}")]
    public ActionResult<Contacto> GetById(int id)
    {
        var contacto = contactoService.ObtenerPorId(id);
        if (contacto == null) return NotFound();
        return Ok(contacto);
    }
    [HttpPost("/add")]
    public ActionResult CrearContacto([FromBody] Contacto nuevoContacto)
    {
        if (nuevoContacto == null)
        {
            return BadRequest("Los datos del contacto son requeridos");
        }
        var contacto = contactoService.Crear(nuevoContacto);
        return CreatedAtAction(nameof(GetById), new { id = contacto.Id }, contacto);

    }

    [HttpDelete("eliminar/{id}")]
    public ActionResult EliminarContacto(int id)
    {
        bool contactoEliminado = contactoService.Eliminar(id);
        if (!contactoEliminado)
        {
            return NotFound($"No se encontro ningun contacto con el ID: {id}");
        }

        return Ok(new { mensaje = "El contacto fue eliminado correctamente" });
    }

    [HttpPut("editar/{id}")]
    public ActionResult EditarContacto(int id, [FromBody] Contacto contactoActualizado)
    {
        bool seEditoContacto = contactoService.Editar(id, contactoActualizado);
        if (seEditoContacto)
        {
            return Ok(new { mensaje = "El contacto fue editado correctamente" });
        }
        return NotFound(new { mensaje = $"No se encontró ningún contacto con el ID: {id}" });
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using ApiContactos.path;
using Microsoft.AspNetCore.Mvc;
namespace ApiContactos.controllers;

[ApiController]
[Route("api/autenticacion")]
public class AutenticacionController : ControllerBase
{
    private readonly AutenticacionService autenticacionService;

    public AutenticacionController(AutenticacionService autenticacionService)
    {
        this.autenticacionService = autenticacionService;
    }

  
    [HttpPost("/registrar")]
    public ActionResult Registrase([FromBody] RegistrarUsuarioDTO usuario)
    {
        var usuarioNuevo = autenticacionService.RegistrarUsuario(usuario);
        if (usuarioNuevo)
        {
            return Ok("Usuario creado con exito");
        }
        else
        {
            return BadRequest("No se pudo crear el usuario");

        }

    }


    [HttpPost("/Login")]

public ActionResult Login ([FromBody] LoginRequest loginRequest)
    {
        var token = autenticacionService.Autenticar(loginRequest);
        if(token == null)
        {
            return Unauthorized("Credenciales incorrectas");
        }
        return Ok (new {Token=token});
    }
       
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nutriRestApi.Models;
using nutriRestApi.XmlRepositorios;

namespace nutriRestApi.Controllers
{
    [Route("nutrirestapi/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly XmlRepositorioCliente repositorio;
        public ClienteController()
        {
            repositorio=new XmlRepositorioCliente("E:/Repositorios/Proyectos/RestAPINutriTec/nutriRestApi/XmlRepositorios/Cliente.xml");
        }
        
        [HttpPost]
        public IActionResult CreateCliente([FromBody] Cliente cliente)
        {
             if (cliente == null)
            {
                return BadRequest("Cliente cannot be null");
            }

            // Guardar el cliente
            repositorio.PostCliente(cliente);

            // Retornar un código de estado 201 (Created) con la URL del nuevo recurso
            return CreatedAtAction(nameof(GetCliente), new { cedula = cliente.cedula }, cliente);
        }

        [HttpGet("{cedula}")]
        public ActionResult<Cliente> GetCliente([FromRoute] int cedula)
        {
            var cliente = repositorio.GetCliente(cedula);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }
    }
}
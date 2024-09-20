using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nutriRestApi.Models;
using nutriRestApi.XmlRepositorios;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace nutriRestApi.Controllers
{
    [Route("nutrirestapi/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly XmlRepositorioCliente repositorio;
        private readonly ILogger<ClienteController> Logger;

        public ClienteController(ILogger<ClienteController> logger) //se inyecta logger como parametro al controler
        {
            Logger=logger;
            string rutaLogs="";
            // Get the current working directory
            string workingDirectory = Environment.CurrentDirectory;
            string pathToFile = Path.Combine(workingDirectory, "DataBaseXML", "Cliente.xml");

            Logger.LogInformation("La ruta a probar es: {workingDirectory}",pathToFile);
            repositorio=new XmlRepositorioCliente(pathToFile);
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
            Logger.LogInformation("Intentando buscar el cliente con cedula: {Cedula}", cedula);
            var cliente = repositorio.GetCliente(cedula);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }
    }
}
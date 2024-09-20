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
    [Route("nutrirestapi/consumoDiario")]
    [ApiController]
    public class ConsumoDiarioController : ControllerBase
    {
        private readonly XmlRepositorioConsumoDiario repositorio;//***************************************
        private readonly ILogger<ConsumoDiarioController> Logger;

        public ConsumoDiarioController(ILogger<ConsumoDiarioController> logger) //se inyecta logger como parametro al controler
        {
            Logger=logger;
            // Get the current working directory
            string workingDirectory = Environment.CurrentDirectory;
            string rutaXml = Path.Combine(workingDirectory, "DataBaseXML", "ConsumoDiario.xml");

            repositorio=new XmlRepositorioConsumoDiario(rutaXml);
        }
        
        [HttpPost]
        public IActionResult CreateConsumoDiario([FromBody] ConsumoDiario consumoDiario)
        {
            Logger.LogInformation("Intentando calcular el consumo diario del {fechaConsumo}",RegistroDiario.fecha);
             if (cliente == null)
            {
                Logger.LogWarning("Se ingreso un cliente null");
                return BadRequest("Cliente cannot be null");
            }

            // Guardar el cliente
            repositorio.PostCliente(cliente);
            Logger.LogInformation("Se creo exitosamente el cliente de cedula {cedCliente}",cliente.cedula);
            // Retornar un código de estado 201 (Created) con la URL del nuevo recurso
            return CreatedAtAction(nameof(GetCliente), new { cedula = cliente.cedula }, cliente);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Logger.LogInformation("Buscando todos los clientes");
            var clientes = repositorio.GetAllClientes();
            if (clientes == null || clientes.Count == 0)
            {
                Logger.LogError("No se encontraron clientes");
                return NotFound("No se encontraron clientes.");
            }
            Logger.LogInformation("Se cargaron todos los clientes con el Get");
            return Ok(clientes);

        }

        [HttpGet("{cedula}")]
        public ActionResult<Cliente> GetCliente([FromRoute] int cedula)
        {
            Logger.LogInformation("Intentando buscar el cliente con cedula: {Cedula}", cedula);
            var cliente = repositorio.GetCliente(cedula);
            if (cliente == null)
            {
                Logger.LogError("El cliente {cedula} no fue encontrado",cedula);
                return NotFound();
            }
            return Ok(cliente);
        }
        [HttpPatch("UpdateCliente/{cedula}")]
        public IActionResult UpdateCliente(int cedula, [FromBody] Dictionary<string, object> valoresActualizar)
        {
            try
            {
                Logger.LogInformation("Intentando editar al cliente de cedula {cedula}",cedula);
                repositorio.UpdateCliente(cedula, valoresActualizar);
                return Ok(new { message = "Cliente actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                Logger.LogError("Hubo un error actualizando al cliente");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
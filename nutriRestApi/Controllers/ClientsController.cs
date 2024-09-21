using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NutriRestApi.Models;
using NutriRestApi.Services;
using System.Collections.Generic;

namespace NutriRestApi.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los Clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService clientService;

        /// <summary>
        /// Constructor del controlador ClientsController.
        /// </summary>
        /// <param name="clientService">Servicio para manejar las operaciones de Cliente.</param>
        public ClientsController(ClientService clientService)
        {
            this.clientService = clientService;
        }

        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        /// <response code="200">Devuelve la lista de clientes.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Client>> Get()
        {
            var clients = clientService.GetAll();
            return Ok(clients);
        }

        /// <summary>
        /// Obtiene un cliente específico por su E_Identifier.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del cliente a obtener.</param>
        /// <returns>El cliente solicitado.</returns>
        /// <response code="200">Devuelve el cliente solicitado.</response>
        /// <response code="404">No se encontró el cliente con el E_Identifier especificado.</response>
        [HttpGet("{eIdentifier}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Client> Get(string eIdentifier)
        {
            var client = clientService.GetByIdentifier(eIdentifier);
            if (client == null)
                return NotFound(new { message = $"Cliente con E_Identifier '{eIdentifier}' no encontrado." });
            return Ok(client);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="client">Datos del cliente a crear.</param>
        /// <returns>El cliente creado.</returns>
        /// <response code="201">Cliente creado exitosamente.</response>
        /// <response code="400">Datos inválidos o E_Domain no válido.</response>
        /// <response code="409">Conflicto: El E_Identifier ya está en uso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Client> Post([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el E_Identifier ya existe
            if (clientService.ClientExistsByIdentifier(client.E_Identifier))
            {
                return Conflict(new { message = $"El E_Identifier '{client.E_Identifier}' ya está en uso." });
            }

            // Verificar que E_Domain no sea 'nutriTECAdmin.com' ni 'nutriTECNutri.com'
            if (client.E_Domain.Equals("nutriTECAdmin.com", System.StringComparison.OrdinalIgnoreCase) ||
                client.E_Domain.Equals("nutriTECNutri.com", System.StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "El E_Domain no puede ser 'nutriTECAdmin.com' ni 'nutriTECNutri.com'." });
            }

            clientService.Add(client);
            return CreatedAtAction(nameof(Get), new { eIdentifier = client.E_Identifier }, client);
        }

        /// <summary>
        /// Actualiza parcialmente un cliente existente.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del cliente a actualizar.</param>
        /// <param name="updatedFields">Campos a actualizar.</param>
        /// <returns>NoContent si se actualizó correctamente.</returns>
        /// <response code="204">Actualización exitosa.</response>
        /// <response code="400">Datos inválidos o E_Domain no válido.</response>
        /// <response code="404">Cliente no encontrado.</response>
        /// <response code="409">Conflicto: El E_Identifier ya está en uso.</response>
        [HttpPatch("{eIdentifier}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Patch(string eIdentifier, [FromBody] Client updatedFields)
        {
            if (updatedFields == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            var existingClient = clientService.GetByIdentifier(eIdentifier);
            if (existingClient == null)
                return NotFound(new { message = $"Cliente con E_Identifier '{eIdentifier}' no encontrado." });

            // Actualizar Email
            if (!string.IsNullOrEmpty(updatedFields.Email))
            {
                existingClient.Email = updatedFields.Email;
            }

            // Actualizar E_Identifier
            if (!string.IsNullOrEmpty(updatedFields.E_Identifier) &&
                !existingClient.E_Identifier.Equals(updatedFields.E_Identifier, System.StringComparison.OrdinalIgnoreCase))
            {
                if (clientService.ClientExistsByIdentifier(updatedFields.E_Identifier))
                {
                    return Conflict(new { message = $"El E_Identifier '{updatedFields.E_Identifier}' ya está en uso." });
                }
                existingClient.E_Identifier = updatedFields.E_Identifier;
            }

            // Actualizar E_Domain
            if (!string.IsNullOrEmpty(updatedFields.E_Domain))
            {
                if (updatedFields.E_Domain.Equals("nutriTECAdmin.com", System.StringComparison.OrdinalIgnoreCase) ||
                    updatedFields.E_Domain.Equals("nutriTECNutri.com", System.StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new { message = "El E_Domain no puede ser 'nutriTECAdmin.com' ni 'nutriTECNutri.com'." });
                }
                existingClient.E_Domain = updatedFields.E_Domain;
            }

            // Actualizar otros campos
            if (updatedFields.FatPercentage != 0)
                existingClient.FatPercentage = updatedFields.FatPercentage;
            if (updatedFields.MaximumDailyConsumption != 0)
                existingClient.MaximumDailyConsumption = updatedFields.MaximumDailyConsumption;
            if (updatedFields.MusclePercentage != 0)
                existingClient.MusclePercentage = updatedFields.MusclePercentage;
            if (!string.IsNullOrEmpty(updatedFields.Country))
                existingClient.Country = updatedFields.Country;
            if (!string.IsNullOrEmpty(updatedFields.InicialMeasures))
                existingClient.InicialMeasures = updatedFields.InicialMeasures;
            if (updatedFields.Im_Hip != 0)
                existingClient.Im_Hip = updatedFields.Im_Hip;
            if (updatedFields.Im_Neck != 0)
                existingClient.Im_Neck = updatedFields.Im_Neck;
            if (updatedFields.Im_Waist != 0)
                existingClient.Im_Waist = updatedFields.Im_Waist;
            if (updatedFields.Imc != 0)
                existingClient.Imc = updatedFields.Imc;
            if (updatedFields.CurrentWeight != 0)
                existingClient.CurrentWeight = updatedFields.CurrentWeight;

            clientService.Update(eIdentifier, existingClient);
            return NoContent();
        }
    }
}
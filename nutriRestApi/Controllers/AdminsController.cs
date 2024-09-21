using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NutriRestApi.Models;
using NutriRestApi.Services;
using System.Collections.Generic;

namespace NutriRestApi.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los Administradores.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly AdminService adminService;

        /// <summary>
        /// Constructor del controlador AdminsController.
        /// </summary>
        /// <param name="adminService">Servicio para manejar las operaciones de Administrador.</param>
        public AdminsController(AdminService adminService)
        {
            this.adminService = adminService;
        }

        /// <summary>
        /// Obtiene todos los administradores.
        /// </summary>
        /// <returns>Lista de administradores.</returns>
        /// <response code="200">Devuelve la lista de administradores.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Admin>> Get()
        {
            var admins = adminService.GetAll();
            return Ok(admins);
        }

        /// <summary>
        /// Obtiene un administrador específico por su E_Identifier.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del administrador a obtener.</param>
        /// <returns>El administrador solicitado.</returns>
        /// <response code="200">Devuelve el administrador solicitado.</response>
        /// <response code="404">No se encontró el administrador con el E_Identifier especificado.</response>
        [HttpGet("{eIdentifier}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Admin> Get(string eIdentifier)
        {
            var admin = adminService.GetByIdentifier(eIdentifier);
            if (admin == null)
                return NotFound(new { message = $"Administrador con E_Identifier '{eIdentifier}' no encontrado." });
            return Ok(admin);
        }

        /// <summary>
        /// Crea un nuevo administrador.
        /// </summary>
        /// <param name="admin">Datos del administrador a crear.</param>
        /// <returns>El administrador creado.</returns>
        /// <response code="201">Administrador creado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="409">Conflicto: El E_Identifier ya está en uso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Admin> Post([FromBody] Admin admin)
        {
            if (admin == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el E_Identifier ya existe
            if (adminService.AdminExistsByIdentifier(admin.E_Identifier))
            {
                return Conflict(new { message = $"El E_Identifier '{admin.E_Identifier}' ya está en uso." });
            }

            // Asegurar que E_Domain sea 'nutriTECAdmin.com'
            if (!admin.E_Domain.Equals("nutriTECAdmin.com", System.StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "El E_Domain debe ser 'nutriTECAdmin.com'." });
            }

            adminService.Add(admin);
            return CreatedAtAction(nameof(Get), new { eIdentifier = admin.E_Identifier }, admin);
        }

        /// <summary>
        /// Actualiza parcialmente un administrador existente.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del administrador a actualizar.</param>
        /// <param name="updatedFields">Campos a actualizar.</param>
        /// <returns>NoContent si la actualización fue exitosa.</returns>
        /// <response code="204">Actualización exitosa.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="404">Administrador no encontrado.</response>
        /// <response code="409">Conflicto: El E_Identifier ya está en uso.</response>
        [HttpPatch("{eIdentifier}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Patch(string eIdentifier, [FromBody] Admin updatedFields)
        {
            if (updatedFields == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            var existingAdmin = adminService.GetByIdentifier(eIdentifier);
            if (existingAdmin == null)
                return NotFound(new { message = $"Administrador con E_Identifier '{eIdentifier}' no encontrado." });

            // Validar el modelo antes de actualizar
            if (!TryValidateModel(existingAdmin))
            {
                return BadRequest(ModelState);
            }

            // Actualizar Email
            if (!string.IsNullOrEmpty(updatedFields.Email))
            {
                existingAdmin.Email = updatedFields.Email;
            }

            // Actualizar E_Identifier
            if (!string.IsNullOrEmpty(updatedFields.E_Identifier) &&
                !existingAdmin.E_Identifier.Equals(updatedFields.E_Identifier, System.StringComparison.OrdinalIgnoreCase))
            {
                if (adminService.AdminExistsByIdentifier(updatedFields.E_Identifier))
                {
                    return Conflict(new { message = $"El E_Identifier '{updatedFields.E_Identifier}' ya está en uso." });
                }
                existingAdmin.E_Identifier = updatedFields.E_Identifier;
            }

            // E_Domain siempre debe ser 'nutriTECAdmin.com'
            existingAdmin.E_Domain = "nutriTECAdmin.com";

            // Guardar los cambios
            adminService.Update(eIdentifier, existingAdmin);
            return NoContent();
        }
    }
}
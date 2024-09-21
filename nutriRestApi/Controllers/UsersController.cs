using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NutriRestApi.Models;
using NutriRestApi.Services;
using System.Collections.Generic;

namespace NutriRestApi.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los Usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;

        /// <summary>
        /// Constructor del controlador UsersController.
        /// </summary>
        /// <param name="userService">Servicio para manejar las operaciones de Usuario.</param>
        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        /// <response code="200">Devuelve la lista de usuarios.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(userService.GetAll());
        }

        /// <summary>
        /// Obtiene un usuario específico por su Id.
        /// </summary>
        /// <param name="id">Id del usuario a obtener.</param>
        /// <returns>El usuario solicitado.</returns>
        /// <response code="200">Devuelve el usuario solicitado.</response>
        /// <response code="404">No se encontró el usuario con el Id especificado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> Get(int id)
        {
            var user = userService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="user">Datos del usuario a crear.</param>
        /// <returns>El usuario creado.</returns>
        /// <response code="201">Usuario creado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="409">Conflicto: El Id o E_Identifier ya está en uso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<User> Post([FromBody] User user)
        {
            if (userService.UserExistsById(user.Id))
            {
                return Conflict(new { message = $"El Id '{user.Id}' ya está en uso." });
            }

            if (userService.UserExistsByIdentifier(user.E_Identifier))
            {
                return Conflict(new { message = $"El E_Identifier '{user.E_Identifier}' ya está en uso." });
            }

            userService.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }


        /// <summary>
        /// Actualiza parcialmente un usuario existente.
        /// </summary>
        /// <param name="id">Id del usuario a actualizar.</param>
        /// <param name="updatedFields">Campos a actualizar.</param>
        /// <returns>NoContent si la actualización fue exitosa.</returns>
        /// <response code="204">Actualización exitosa.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="404">Usuario no encontrado.</response>
        /// <response code="409">Conflicto: El E_Identifier ya está en uso.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, [FromBody] User updatedFields)
        {
            if (updatedFields == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            var existingUser = userService.GetById(id);
            if (existingUser == null)
                return NotFound(new { message = $"Usuario con Id '{id}' no encontrado." });

            // Validar el modelo
            if (!TryValidateModel(existingUser))
            {
                return BadRequest(ModelState);
            }

            // Actualizar campos individualmente si se proporcionan
            if (updatedFields.Age != 0)
            {
                existingUser.Age = updatedFields.Age;
            }

            if (!string.IsNullOrEmpty(updatedFields.Password))
            {
                existingUser.Password = updatedFields.Password;
            }

            if (!string.IsNullOrEmpty(updatedFields.Email))
            {
                existingUser.Email = updatedFields.Email;
            }

            if (!string.IsNullOrEmpty(updatedFields.Birthdate))
            {
                existingUser.Birthdate = updatedFields.Birthdate;
            }

            if (updatedFields.B_Day != 0)
            {
                existingUser.B_Day = updatedFields.B_Day;
            }

            if (updatedFields.B_Month != 0)
            {
                existingUser.B_Month = updatedFields.B_Month;
            }

            if (updatedFields.B_Year != 0)
            {
                existingUser.B_Year = updatedFields.B_Year;
            }

            if (!string.IsNullOrEmpty(updatedFields.Fullname))
            {
                existingUser.Fullname = updatedFields.Fullname;
            }

            if (!string.IsNullOrEmpty(updatedFields.Name))
            {
                existingUser.Name = updatedFields.Name;
            }

            if (!string.IsNullOrEmpty(updatedFields.FirstlastName))
            {
                existingUser.FirstlastName = updatedFields.FirstlastName;
            }

            if (!string.IsNullOrEmpty(updatedFields.SecondlastName))
            {
                existingUser.SecondlastName = updatedFields.SecondlastName;
            }

            if (!string.IsNullOrEmpty(updatedFields.Username))
            {
                existingUser.Username = updatedFields.Username;
            }

            // Actualizar E_Identifier y verificar unicidad
            if (!string.IsNullOrEmpty(updatedFields.E_Identifier) && 
                !existingUser.E_Identifier.Equals(updatedFields.E_Identifier, System.StringComparison.OrdinalIgnoreCase))
            {
                if (userService.UserExistsByIdentifier(updatedFields.E_Identifier))
                {
                    return Conflict(new { message = $"El E_Identifier '{updatedFields.E_Identifier}' ya está en uso." });
                }
                existingUser.E_Identifier = updatedFields.E_Identifier;
            }

            // Actualizar E_Domain si se proporciona y es válida
            if (!string.IsNullOrEmpty(updatedFields.E_Domain))
            {
                existingUser.E_Domain = updatedFields.E_Domain;
            }

            // Guardar los cambios
            userService.Update(id, existingUser);
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NutriRestApi.Models;
using NutriRestApi.Services;
using System.Collections.Generic;

namespace NutriRestApi.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los Platos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly DishService dishService;

        /// <summary>
        /// Constructor del controlador DishesController.
        /// </summary>
        /// <param name="dishService">Servicio para manejar las operaciones de Plato.</param>
        public DishesController(DishService dishService)
        {
            this.dishService = dishService;
        }

        /// <summary>
        /// Obtiene todos los platos.
        /// </summary>
        /// <returns>Lista de platos.</returns>
        /// <response code="200">Devuelve la lista de platos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Dish>> Get()
        {
            var dishes = dishService.GetAll();
            return Ok(dishes);
        }

        /// <summary>
        /// Obtiene un plato específico por su BarCode.
        /// </summary>
        /// <param name="barCode">BarCode del plato a obtener.</param>
        /// <returns>El plato solicitado.</returns>
        /// <response code="200">Devuelve el plato solicitado.</response>
        /// <response code="404">No se encontró el plato con el BarCode especificado.</response>
        [HttpGet("{barCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Dish> Get(long barCode)
        {
            var dish = dishService.GetByBarCode(barCode);
            if (dish == null)
                return NotFound(new { message = $"Plato con BarCode '{barCode}' no encontrado." });
            return Ok(dish);
        }

        /// <summary>
        /// Crea un nuevo plato.
        /// </summary>
        /// <param name="dish">Datos del plato a crear (sin BarCode).</param>
        /// <returns>El plato creado.</returns>
        /// <response code="201">Plato creado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Dish> Post([FromBody] Dish dish)
        {
            if (dish == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Agregar el plato, el BarCode se genera automáticamente
            dishService.Add(dish);
            return CreatedAtAction(nameof(Get), new { barCode = dish.BarCode }, dish);
        }

        /// <summary>
        /// Actualiza parcialmente un plato existente.
        /// </summary>
        /// <param name="barCode">BarCode del plato a actualizar.</param>
        /// <param name="updatedDish">Datos del plato actualizados (incluye BarCode).</param>
        /// <returns>NoContent si se actualizó correctamente.</returns>
        /// <response code="204">Actualización exitosa.</response>
        /// <response code="400">Datos inválidos o BarCode no coincide.</response>
        /// <response code="404">Plato no encontrado.</response>
        /// <response code="409">Conflicto: El BarCode ya está en uso.</response>
        [HttpPatch("{barCode}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Patch(long barCode, [FromBody] Dish updatedDish)
        {
            if (updatedDish == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            // Verificar que el BarCode en el cuerpo coincide con el de la ruta
            if (updatedDish.BarCode != barCode)
            {
                return BadRequest(new { message = "El BarCode en el cuerpo no coincide con el de la ruta." });
            }

            var existingDish = dishService.GetByBarCode(barCode);
            if (existingDish == null)
                return NotFound(new { message = $"Plato con BarCode '{barCode}' no encontrado." });

            // Validar el modelo antes de actualizar
            if (!TryValidateModel(existingDish))
            {
                return BadRequest(ModelState);
            }

            // Actualizar campos
            existingDish.Name = !string.IsNullOrEmpty(updatedDish.Name) ? updatedDish.Name : existingDish.Name;
            existingDish.Description = updatedDish.Description ?? existingDish.Description;
            existingDish.Calcium = updatedDish.Calcium != 0 ? updatedDish.Calcium : existingDish.Calcium;
            existingDish.Sodium = updatedDish.Sodium != 0 ? updatedDish.Sodium : existingDish.Sodium;
            existingDish.Fat = updatedDish.Fat != 0 ? updatedDish.Fat : existingDish.Fat;
            existingDish.Energy = updatedDish.Energy != 0 ? updatedDish.Energy : existingDish.Energy;
            existingDish.ServingSize = updatedDish.ServingSize != 0 ? updatedDish.ServingSize : existingDish.ServingSize;
            existingDish.Iron = updatedDish.Iron != 0 ? updatedDish.Iron : existingDish.Iron;
            existingDish.Protein = updatedDish.Protein != 0 ? updatedDish.Protein : existingDish.Protein;
            existingDish.Carbohydrates = updatedDish.Carbohydrates != 0 ? updatedDish.Carbohydrates : existingDish.Carbohydrates;
            existingDish.Status = !string.IsNullOrEmpty(updatedDish.Status) ? updatedDish.Status : existingDish.Status;

            // Actualizar Products
            if (updatedDish.Products != null)
            {
                existingDish.Products = updatedDish.Products;
            }

            // Guardar los cambios
            dishService.Update(barCode, existingDish);
            return NoContent();
        }
    }
}
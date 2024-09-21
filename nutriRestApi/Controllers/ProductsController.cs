using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NutriRestApi.Models;
using NutriRestApi.Services;
using System.Collections.Generic;

namespace NutriRestApi.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los Productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService productService;

        /// <summary>
        /// Constructor del controlador ProductsController.
        /// </summary>
        /// <param name="productService">Servicio para manejar las operaciones de Producto.</param>
        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        /// <response code="200">Devuelve la lista de productos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = productService.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// Obtiene un producto específico por su BarCode.
        /// </summary>
        /// <param name="barCode">BarCode del producto a obtener.</param>
        /// <returns>El producto solicitado.</returns>
        /// <response code="200">Devuelve el producto solicitado.</response>
        /// <response code="404">No se encontró el producto con el BarCode especificado.</response>
        [HttpGet("{barCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> Get(long barCode)
        {
            var product = productService.GetByBarCode(barCode);
            if (product == null)
                return NotFound(new { message = $"Producto con BarCode '{barCode}' no encontrado." });
            return Ok(product);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="product">Datos del producto a crear (sin BarCode).</param>
        /// <returns>El producto creado.</returns>
        /// <response code="201">Producto creado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="409">Conflicto: El BarCode ya está en uso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Agregar el producto, el BarCode se genera automáticamente
            productService.Add(product);
            return CreatedAtAction(nameof(Get), new { barCode = product.BarCode }, product);
        }

        /// <summary>
        /// Actualiza parcialmente un producto existente.
        /// </summary>
        /// <param name="barCode">BarCode del producto a actualizar.</param>
        /// <param name="updatedProduct">Datos del producto actualizados (incluye BarCode).</param>
        /// <returns>NoContent si se actualizó correctamente.</returns>
        /// <response code="204">Actualización exitosa.</response>
        /// <response code="400">Datos inválidos o BarCode no coincide.</response>
        /// <response code="404">Producto no encontrado.</response>
        /// <response code="409">Conflicto: El BarCode ya está en uso.</response>
        [HttpPatch("{barCode}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Patch(long barCode, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            // Verificar que el BarCode en el cuerpo coincide con el de la ruta
            if (updatedProduct.BarCode != barCode)
            {
                return BadRequest(new { message = "El BarCode en el cuerpo no coincide con el de la ruta." });
            }

            var existingProduct = productService.GetByBarCode(barCode);
            if (existingProduct == null)
                return NotFound(new { message = $"Producto con BarCode '{barCode}' no encontrado." });

            // Validar el modelo antes de actualizar
            if (!TryValidateModel(existingProduct))
            {
                return BadRequest(ModelState);
            }

            // Actualizar campos
            existingProduct.Name = !string.IsNullOrEmpty(updatedProduct.Name) ? updatedProduct.Name : existingProduct.Name;
            existingProduct.Description = updatedProduct.Description ?? existingProduct.Description;
            existingProduct.Calcium = updatedProduct.Calcium != 0 ? updatedProduct.Calcium : existingProduct.Calcium;
            existingProduct.Sodium = updatedProduct.Sodium != 0 ? updatedProduct.Sodium : existingProduct.Sodium;
            existingProduct.Fat = updatedProduct.Fat != 0 ? updatedProduct.Fat : existingProduct.Fat;
            existingProduct.Energy = updatedProduct.Energy != 0 ? updatedProduct.Energy : existingProduct.Energy;
            existingProduct.ServingSize = updatedProduct.ServingSize != 0 ? updatedProduct.ServingSize : existingProduct.ServingSize;
            existingProduct.Iron = updatedProduct.Iron != 0 ? updatedProduct.Iron : existingProduct.Iron;
            existingProduct.Protein = updatedProduct.Protein != 0 ? updatedProduct.Protein : existingProduct.Protein;
            existingProduct.Carbohydrates = updatedProduct.Carbohydrates != 0 ? updatedProduct.Carbohydrates : existingProduct.Carbohydrates;
            existingProduct.Status = !string.IsNullOrEmpty(updatedProduct.Status) ? updatedProduct.Status : existingProduct.Status;

            productService.Update(barCode, existingProduct);
            return NoContent();
        }
    }
}
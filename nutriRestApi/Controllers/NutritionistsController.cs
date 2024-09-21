using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NutriRestApi.Models;
using NutriRestApi.Services;
using System.Collections.Generic;

namespace NutriRestApi.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones relacionadas con los Nutricionistas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionistsController : ControllerBase
    {
        private readonly NutritionistService nutritionistService;

        /// <summary>
        /// Constructor del controlador NutritionistsController.
        /// </summary>
        /// <param name="nutritionistService">Servicio para manejar las operaciones de Nutricionista.</param>
        public NutritionistsController(NutritionistService nutritionistService)
        {
            this.nutritionistService = nutritionistService;
        }

        /// <summary>
        /// Obtiene todos los nutricionistas.
        /// </summary>
        /// <returns>Lista de nutricionistas.</returns>
        /// <response code="200">Devuelve la lista de nutricionistas.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Nutritionist>> Get()
        {
            var nutritionists = nutritionistService.GetAll();
            return Ok(nutritionists);
        }

        /// <summary>
        /// Obtiene un nutricionista específico por su E_Identifier.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del nutricionista a obtener.</param>
        /// <returns>El nutricionista solicitado.</returns>
        /// <response code="200">Devuelve el nutricionista solicitado.</response>
        /// <response code="404">No se encontró el nutricionista con el E_Identifier especificado.</response>
        [HttpGet("{eIdentifier}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Nutritionist> Get(string eIdentifier)
        {
            var nutritionist = nutritionistService.GetByIdentifier(eIdentifier);
            if (nutritionist == null)
                return NotFound(new { message = $"Nutricionista con E_Identifier '{eIdentifier}' no encontrado." });
            return Ok(nutritionist);
        }

        /// <summary>
        /// Crea un nuevo nutricionista.
        /// </summary>
        /// <param name="nutritionist">Datos del nutricionista a crear.</param>
        /// <returns>El nutricionista creado.</returns>
        /// <response code="201">Nutricionista creado exitosamente.</response>
        /// <response code="400">Datos inválidos o E_Domain no válido.</response>
        /// <response code="409">Conflicto: El E_Identifier ya está en uso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Nutritionist> Post([FromBody] Nutritionist nutritionist)
        {
            if (nutritionist == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el E_Identifier ya existe
            if (nutritionistService.NutritionistExistsByIdentifier(nutritionist.E_Identifier))
            {
                return Conflict(new { message = $"El E_Identifier '{nutritionist.E_Identifier}' ya está en uso." });
            }

            // Verificar que E_Domain sea 'nutriTECNutri.com'
            if (!nutritionist.E_Domain.Equals("nutriTECNutri.com", System.StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "El E_Domain debe ser 'nutriTECNutri.com'." });
            }

            nutritionistService.Add(nutritionist);
            return CreatedAtAction(nameof(Get), new { eIdentifier = nutritionist.E_Identifier }, nutritionist);
        }

        /// <summary>
        /// Verifica si un código de nutricionista es válido.
        /// </summary>
        /// <param name="code">Código a verificar.</param>
        /// <returns>True si el código existe; de lo contrario, false.</returns>
        /// <response code="200">Devuelve el resultado de la verificación.</response>
        [HttpGet("verifyCode/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> VerifyCode(int code)
        {
            var isValid = nutritionistService.VerifyCode(code);
            return Ok(isValid);
        }

        /// <summary>
        /// Actualiza parcialmente un nutricionista existente.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del nutricionista a actualizar.</param>
        /// <param name="updatedFields">Campos a actualizar.</param>
        /// <returns>NoContent si se actualizó correctamente.</returns>
        /// <response code="204">Actualización exitosa.</response>
        /// <response code="400">Datos inválidos o E_Domain no válido.</response>
        /// <response code="404">Nutricionista no encontrado.</response>
        /// <response code="409">Conflicto: El E_Identifier ya está en uso.</response>
        [HttpPatch("{eIdentifier}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Patch(string eIdentifier, [FromBody] Nutritionist updatedFields)
        {
            if (updatedFields == null)
            {
                return BadRequest(new { message = "Los datos proporcionados son nulos." });
            }

            var existingNutritionist = nutritionistService.GetByIdentifier(eIdentifier);
            if (existingNutritionist == null)
                return NotFound(new { message = $"Nutricionista con E_Identifier '{eIdentifier}' no encontrado." });

            // Validar el modelo antes de actualizar
            if (!TryValidateModel(updatedFields))
            {
                return BadRequest(ModelState);
            }

            // Actualizar Email
            if (!string.IsNullOrEmpty(updatedFields.Email))
            {
                existingNutritionist.Email = updatedFields.Email;
            }

            // E_Domain siempre debe ser 'nutriTECNutri.com'
            existingNutritionist.E_Domain = "nutriTECNutri.com";

            // Actualizar Weight
            if (updatedFields.Weight != 0)
            {
                existingNutritionist.Weight = updatedFields.Weight;
            }

            // Actualizar IMC
            if (updatedFields.Imc != 0)
            {
                existingNutritionist.Imc = updatedFields.Imc;
            }

            // Actualizar Address
            if (!string.IsNullOrEmpty(updatedFields.Address))
            {
                existingNutritionist.Address = updatedFields.Address;
            }

            // Actualizar A_Province
            if (!string.IsNullOrEmpty(updatedFields.A_Province))
            {
                existingNutritionist.A_Province = updatedFields.A_Province;
            }

            // Actualizar A_Canton
            if (!string.IsNullOrEmpty(updatedFields.A_Canton))
            {
                existingNutritionist.A_Canton = updatedFields.A_Canton;
            }

            // Actualizar A_District
            if (!string.IsNullOrEmpty(updatedFields.A_District))
            {
                existingNutritionist.A_District = updatedFields.A_District;
            }

            // Actualizar Photo
            if (!string.IsNullOrEmpty(updatedFields.Photo))
            {
                existingNutritionist.Photo = updatedFields.Photo;
            }

            // Actualizar PaymentCard
            if (!string.IsNullOrEmpty(updatedFields.PaymentCard))
            {
                existingNutritionist.PaymentCard = updatedFields.PaymentCard;
            }

            // Actualizar Pc_Name
            if (!string.IsNullOrEmpty(updatedFields.Pc_Name))
            {
                existingNutritionist.Pc_Name = updatedFields.Pc_Name;
            }

            // Actualizar Pc_Number
            if (updatedFields.Pc_Number != 0)
            {
                existingNutritionist.Pc_Number = updatedFields.Pc_Number;
            }

            // Actualizar Pc_Cvc
            if (updatedFields.Pc_Cvc != 0)
            {
                existingNutritionist.Pc_Cvc = updatedFields.Pc_Cvc;
            }

            // Actualizar Pc_ExpirationDate
            if (!string.IsNullOrEmpty(updatedFields.Pc_ExpirationDate))
            {
                existingNutritionist.Pc_ExpirationDate = updatedFields.Pc_ExpirationDate;
            }

            // Actualizar Pc_Ed_Year
            if (updatedFields.Pc_Ed_Year != 0)
            {
                existingNutritionist.Pc_Ed_Year = updatedFields.Pc_Ed_Year;
            }

            // Actualizar Pc_Ed_Month
            if (updatedFields.Pc_Ed_Month != 0)
            {
                existingNutritionist.Pc_Ed_Month = updatedFields.Pc_Ed_Month;
            }

            // Actualizar PaymentType
            if (!string.IsNullOrEmpty(updatedFields.PaymentType))
            {
                existingNutritionist.PaymentType = updatedFields.PaymentType;
            }

            // Actualizar TotalPaymentAmount
            if (updatedFields.TotalPaymentAmount != 0)
            {
                existingNutritionist.TotalPaymentAmount = updatedFields.TotalPaymentAmount;
            }

            // Actualizar Discount
            if (updatedFields.Discount != 0)
            {
                existingNutritionist.Discount = updatedFields.Discount;
            }

            // Actualizar FinalPayment
            if (updatedFields.FinalPayment != 0)
            {
                existingNutritionist.FinalPayment = updatedFields.FinalPayment;
            }

            // Actualizar Code si no se proporciona
            if (updatedFields.Code != 0)
            {
                existingNutritionist.Code = updatedFields.Code;
            }
            else
            {
                // Generar Code si no se proporciona
                if (existingNutritionist.Code == 0)
                {
                    existingNutritionist.Code = nutritionistService.GenerateUniqueCode();
                }
            }

            // Actualizar Advicer
            if (updatedFields.Advicer != null)
            {
                existingNutritionist.Advicer = updatedFields.Advicer;
            }

            // Guardar los cambios
            nutritionistService.Update(eIdentifier, existingNutritionist);
            return NoContent();
        }
    }
}
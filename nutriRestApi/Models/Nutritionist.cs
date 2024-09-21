using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutriRestApi.Models
{
    /// <summary>
    /// Representa un nutricionista en el sistema.
    /// </summary>
    public class Nutritionist
    {
        /// <summary>
        /// Correo electrónico del nutricionista.
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Identificador único del nutricionista.
        /// </summary>
        [Required]
        public string E_Identifier { get; set; }

        /// <summary>
        /// Dominio del nutricionista, debe ser 'nutriTECNutri.com'.
        /// </summary>
        [Required]
        [RegularExpression(@"^nutriTECNutri\.com$", ErrorMessage = "El E_Domain debe ser 'nutriTECNutri.com'.")]
        public string E_Domain { get; set; } = "nutriTECNutri.com";

        /// <summary>
        /// Peso del nutricionista.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Índice de masa corporal del nutricionista.
        /// </summary>
        public double Imc { get; set; }

        /// <summary>
        /// Dirección del nutricionista.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Provincia del nutricionista.
        /// </summary>
        public string A_Province { get; set; }

        /// <summary>
        /// Cantón del nutricionista.
        /// </summary>
        public string A_Canton { get; set; }

        /// <summary>
        /// Distrito del nutricionista.
        /// </summary>
        public string A_District { get; set; }

        /// <summary>
        /// Foto del nutricionista.
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Tarjeta de pago del nutricionista.
        /// </summary>
        public string PaymentCard { get; set; }

        /// <summary>
        /// Nombre en la tarjeta de pago.
        /// </summary>
        public string Pc_Name { get; set; }

        /// <summary>
        /// Número de la tarjeta de pago.
        /// </summary>
        public long Pc_Number { get; set; }

        /// <summary>
        /// CVC de la tarjeta de pago.
        /// </summary>
        public int Pc_Cvc { get; set; }

        /// <summary>
        /// Fecha de expiración de la tarjeta de pago.
        /// </summary>
        public string Pc_ExpirationDate { get; set; }

        /// <summary>
        /// Año de expiración de la tarjeta de pago.
        /// </summary>
        public int Pc_Ed_Year { get; set; }

        /// <summary>
        /// Mes de expiración de la tarjeta de pago.
        /// </summary>
        public int Pc_Ed_Month { get; set; }

        /// <summary>
        /// Tipo de pago.
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// Monto total del pago.
        /// </summary>
        public double TotalPaymentAmount { get; set; }

        /// <summary>
        /// Descuento aplicado.
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// Pago final después de descuentos.
        /// </summary>
        public double FinalPayment { get; set; }

        /// <summary>
        /// Código único generado automáticamente si no se proporciona.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Lista de clientes asesorados por el nutricionista.
        /// </summary>
        public List<Client> Advicer { get; set; } = new List<Client>();
    }
}
using System.ComponentModel.DataAnnotations;

namespace NutriRestApi.Models
{
    /// <summary>
    /// Representa un cliente en el sistema.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Correo electrónico del cliente.
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Identificador único del cliente.
        /// </summary>
        [Required]
        public string E_Identifier { get; set; }

        /// <summary>
        /// Dominio del cliente, no puede ser 'nutriTECAdmin.com' ni 'nutriTECNutri.com'.
        /// </summary>
        [Required]
        [RegularExpression(@"^(?!nutriTECAdmin\.com$)(?!nutriTECNutri\.com$).+$", ErrorMessage = "El E_Domain no puede ser 'nutriTECAdmin.com' ni 'nutriTECNutri.com'.")]
        public string E_Domain { get; set; }

        /// <summary>
        /// Porcentaje de grasa del cliente.
        /// </summary>
        public double FatPercentage { get; set; }

        /// <summary>
        /// Consumo diario máximo del cliente.
        /// </summary>
        public double MaximumDailyConsumption { get; set; }

        /// <summary>
        /// Porcentaje de músculo del cliente.
        /// </summary>
        public double MusclePercentage { get; set; }

        /// <summary>
        /// País del cliente.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Medidas iniciales del cliente.
        /// </summary>
        public string InicialMeasures { get; set; }

        /// <summary>
        /// Medida de la cadera del cliente.
        /// </summary>
        public double Im_Hip { get; set; }

        /// <summary>
        /// Medida del cuello del cliente.
        /// </summary>
        public double Im_Neck { get; set; }

        /// <summary>
        /// Medida de la cintura del cliente.
        /// </summary>
        public double Im_Waist { get; set; }

        /// <summary>
        /// Índice de masa corporal del cliente.
        /// </summary>
        public double Imc { get; set; }

        /// <summary>
        /// Peso actual del cliente.
        /// </summary>
        public double CurrentWeight { get; set; }
    }
}
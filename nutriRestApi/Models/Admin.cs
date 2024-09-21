using System.ComponentModel.DataAnnotations;

namespace NutriRestApi.Models
{
    /// <summary>
    /// Representa un administrador en el sistema.
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Correo electrónico del administrador.
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Identificador único del administrador.
        /// </summary>
        [Required]
        public string E_Identifier { get; set; }

        /// <summary>
        /// Dominio del administrador, debe ser 'nutriTECAdmin.com'.
        /// </summary>
        [Required]
        [RegularExpression(@"^nutriTECAdmin\.com$", ErrorMessage = "El e_domain debe ser 'nutriTECAdmin.com'.")]
        public string E_Domain { get; set; } = "nutriTECAdmin.com";
    }
}
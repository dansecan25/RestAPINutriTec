using System.ComponentModel.DataAnnotations;

namespace NutriRestApi.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0, 150, ErrorMessage = "La edad debe estar entre 0 y 150.")]
        public int Age { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres.")]
        public string Password { get; set; }

        [Required]
        public string Birthdate { get; set; }

        public int B_Day { get; set; }
        public int B_Month { get; set; }
        public int B_Year { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required]
        public string E_Identifier { get; set; }

        [Required]
        public string E_Domain { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FirstlastName { get; set; }

        [Required]
        public string SecondlastName { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
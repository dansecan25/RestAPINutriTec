using System.ComponentModel.DataAnnotations;

namespace NutriRestApi.Models
{
    /// <summary>
    /// Representa un producto en el sistema.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Código de barras único del producto.
        /// </summary>
        public long BarCode { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Name { get; set; }

        /// <summary>
        /// Descripción del producto.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Contenido de calcio en el producto.
        /// </summary>
        public double Calcium { get; set; }

        /// <summary>
        /// Contenido de sodio en el producto.
        /// </summary>
        public double Sodium { get; set; }

        /// <summary>
        /// Contenido de grasa en el producto.
        /// </summary>
        public double Fat { get; set; }

        /// <summary>
        /// Energía proporcionada por el producto.
        /// </summary>
        public double Energy { get; set; }

        /// <summary>
        /// Tamaño de la porción del producto.
        /// </summary>
        public double ServingSize { get; set; }

        /// <summary>
        /// Contenido de hierro en el producto.
        /// </summary>
        public double Iron { get; set; }

        /// <summary>
        /// Contenido de proteína en el producto.
        /// </summary>
        public double Protein { get; set; }

        /// <summary>
        /// Contenido de carbohidratos en el producto.
        /// </summary>
        public double Carbohydrates { get; set; }

        /// <summary>
        /// Estado del producto.
        /// </summary>
        [Required(ErrorMessage = "El estado del producto es obligatorio.")]
        public string Status { get; set; }
    }
}
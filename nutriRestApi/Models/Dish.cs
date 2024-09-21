using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutriRestApi.Models
{
    /// <summary>
    /// Representa un plato en el sistema.
    /// </summary>
    public class Dish
    {
        /// <summary>
        /// Código de barras único del plato.
        /// </summary>
        public long BarCode { get; set; }

        /// <summary>
        /// Nombre del plato.
        /// </summary>
        [Required(ErrorMessage = "El nombre del plato es obligatorio.")]
        public string Name { get; set; }

        /// <summary>
        /// Descripción del plato.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Contenido de calcio en el plato.
        /// </summary>
        public double Calcium { get; set; }

        /// <summary>
        /// Contenido de sodio en el plato.
        /// </summary>
        public double Sodium { get; set; }

        /// <summary>
        /// Contenido de grasa en el plato.
        /// </summary>
        public double Fat { get; set; }

        /// <summary>
        /// Energía proporcionada por el plato.
        /// </summary>
        public double Energy { get; set; }

        /// <summary>
        /// Tamaño de la porción del plato.
        /// </summary>
        public double ServingSize { get; set; }

        /// <summary>
        /// Contenido de hierro en el plato.
        /// </summary>
        public double Iron { get; set; }

        /// <summary>
        /// Contenido de proteína en el plato.
        /// </summary>
        public double Protein { get; set; }

        /// <summary>
        /// Contenido de carbohidratos en el plato.
        /// </summary>
        public double Carbohydrates { get; set; }

        /// <summary>
        /// Estado del plato.
        /// </summary>
        [Required(ErrorMessage = "El estado del plato es obligatorio.")]
        public string Status { get; set; }

        /// <summary>
        /// Lista de productos incluidos en el plato con sus cantidades.
        /// </summary>
        public List<DishProduct> Products { get; set; } = new List<DishProduct>();
    }

    /// <summary>
    /// Representa un producto y su cantidad dentro de un plato.
    /// </summary>
    public class DishProduct
    {
        /// <summary>
        /// Producto incluido en el plato.
        /// </summary>
        [Required]
        public Product Product { get; set; }

        /// <summary>
        /// Cantidad del producto en el plato.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Quantity { get; set; }
    }
}
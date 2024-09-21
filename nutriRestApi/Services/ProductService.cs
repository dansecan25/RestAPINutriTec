using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NutriRestApi.Models;

namespace NutriRestApi.Services
{
    /// <summary>
    /// Servicio para manejar operaciones CRUD de Productos.
    /// </summary>
    public class ProductService
    {
        private readonly string jsonFilePath = "Data/products.json";
        private List<Product> products;

        /// <summary>
        /// Constructor que inicializa la lista de productos desde un archivo JSON.
        /// </summary>
        public ProductService()
        {
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                products = JsonSerializer.Deserialize<List<Product>>(json);
            }
            else
            {
                products = new List<Product>();
                SaveChanges(); // Crear el archivo JSON si no existe
            }
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        /// <summary>
        /// Obtiene un producto por su BarCode.
        /// </summary>
        /// <param name="barCode">BarCode del producto.</param>
        /// <returns>Producto si se encuentra; de lo contrario, null.</returns>
        public Product GetByBarCode(long barCode)
        {
            return products.Find(p => p.BarCode == barCode);
        }

        /// <summary>
        /// Agrega un nuevo producto.
        /// </summary>
        /// <param name="product">Producto a agregar.</param>
        public void Add(Product product)
        {
            // Generar BarCode único
            product.BarCode = GenerateUniqueBarCode();
            products.Add(product);
            SaveChanges();
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="barCode">BarCode del producto a actualizar.</param>
        /// <param name="updatedProduct">Producto actualizado.</param>
        public void Update(long barCode, Product updatedProduct)
        {
            var index = products.FindIndex(p => p.BarCode == barCode);
            if (index != -1)
            {
                products[index] = updatedProduct;
                SaveChanges();
            }
        }

        /// <summary>
        /// Verifica si un BarCode ya está en uso.
        /// </summary>
        /// <param name="barCode">BarCode a verificar.</param>
        /// <returns>True si ya está en uso; de lo contrario, false.</returns>
        public bool ProductExistsByBarCode(long barCode)
        {
            return products.Any(p => p.BarCode == barCode);
        }

        /// <summary>
        /// Genera un BarCode único.
        /// </summary>
        /// <returns>BarCode único.</returns>
        public long GenerateUniqueBarCode()
        {
            long newBarCode;
            var rand = new System.Random();
            do
            {
                newBarCode = rand.NextInt64(1000000000000, 9999999999999); // Genera un BarCode de 13 dígitos
            } while (ProductExistsByBarCode(newBarCode));
            return newBarCode;
        }

        /// <summary>
        /// Guarda los cambios en el archivo JSON.
        /// </summary>
        private void SaveChanges()
        {
            try
            {
                var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));
                File.WriteAllText(jsonFilePath, json);
                Console.WriteLine("Cambios guardados en products.json.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                throw;
            }
        }
    }
}
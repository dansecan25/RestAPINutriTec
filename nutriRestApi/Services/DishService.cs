using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NutriRestApi.Models;

namespace NutriRestApi.Services
{
    /// <summary>
    /// Servicio para manejar operaciones CRUD de Platos.
    /// </summary>
    public class DishService
    {
        private readonly string jsonFilePath = "Data/dishes.json";
        private List<Dish> dishes;

        /// <summary>
        /// Constructor que inicializa la lista de platos desde un archivo JSON.
        /// </summary>
        public DishService()
        {
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                dishes = JsonSerializer.Deserialize<List<Dish>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                dishes = new List<Dish>();
                SaveChanges(); // Crear el archivo JSON si no existe
            }
        }

        /// <summary>
        /// Obtiene todos los platos.
        /// </summary>
        /// <returns>Lista de platos.</returns>
        public IEnumerable<Dish> GetAll()
        {
            return dishes;
        }

        /// <summary>
        /// Obtiene un plato por su BarCode.
        /// </summary>
        /// <param name="barCode">BarCode del plato.</param>
        /// <returns>Plato si se encuentra; de lo contrario, null.</returns>
        public Dish GetByBarCode(long barCode)
        {
            return dishes.Find(d => d.BarCode == barCode);
        }

        /// <summary>
        /// Agrega un nuevo plato.
        /// </summary>
        /// <param name="dish">Plato a agregar.</param>
        public void Add(Dish dish)
        {
            // Generar BarCode único
            dish.BarCode = GenerateUniqueBarCode();
            dishes.Add(dish);
            SaveChanges();
        }

        /// <summary>
        /// Actualiza un plato existente.
        /// </summary>
        /// <param name="barCode">BarCode del plato a actualizar.</param>
        /// <param name="updatedDish">Plato actualizado.</param>
        public void Update(long barCode, Dish updatedDish)
        {
            var index = dishes.FindIndex(d => d.BarCode == barCode);
            if (index != -1)
            {
                dishes[index] = updatedDish;
                SaveChanges();
            }
        }

        /// <summary>
        /// Verifica si un BarCode ya está en uso.
        /// </summary>
        /// <param name="barCode">BarCode a verificar.</param>
        /// <returns>True si ya está en uso; de lo contrario, false.</returns>
        public bool DishExistsByBarCode(long barCode)
        {
            return dishes.Any(d => d.BarCode == barCode);
        }

        /// <summary>
        /// Genera un BarCode único.
        /// </summary>
        /// <returns>BarCode único.</returns>
        public long GenerateUniqueBarCode()
        {
            long newBarCode;
            var rand = new Random();
            do
            {
                newBarCode = rand.NextInt64(1000000000000, 9999999999999); // Genera un BarCode de 13 dígitos
            } while (DishExistsByBarCode(newBarCode));
            return newBarCode;
        }

        /// <summary>
        /// Guarda los cambios en el archivo JSON.
        /// </summary>
        private void SaveChanges()
        {
            try
            {
                var json = JsonSerializer.Serialize(dishes, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));
                File.WriteAllText(jsonFilePath, json);
                Console.WriteLine("Cambios guardados en dishes.json.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NutriRestApi.Models;

namespace NutriRestApi.Services
{
    /// <summary>
    /// Servicio para manejar operaciones CRUD de Nutricionistas.
    /// </summary>
    public class NutritionistService
    {
        private readonly string jsonFilePath = "Data/nutritionists.json";
        private List<Nutritionist> nutritionists;

        /// <summary>
        /// Constructor que inicializa la lista de nutricionistas desde un archivo JSON.
        /// </summary>
        public NutritionistService()
        {
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                nutritionists = JsonSerializer.Deserialize<List<Nutritionist>>(json);
            }
            else
            {
                nutritionists = new List<Nutritionist>();
                SaveChanges(); // Crear el archivo JSON si no existe
            }
        }

        /// <summary>
        /// Obtiene todos los nutricionistas.
        /// </summary>
        /// <returns>Lista de nutricionistas.</returns>
        public IEnumerable<Nutritionist> GetAll()
        {
            return nutritionists;
        }

        /// <summary>
        /// Obtiene un nutricionista por su E_Identifier.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del nutricionista.</param>
        /// <returns>Nutricionista si se encuentra; de lo contrario, null.</returns>
        public Nutritionist GetByIdentifier(string eIdentifier)
        {
            return nutritionists.Find(n => n.E_Identifier.Equals(eIdentifier, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Agrega un nuevo nutricionista.
        /// </summary>
        /// <param name="nutritionist">Nutricionista a agregar.</param>
        public void Add(Nutritionist nutritionist)
        {
            // Generar Code si no se proporciona
            if (nutritionist.Code == 0)
            {
                nutritionist.Code = GenerateUniqueCode();
            }
            nutritionists.Add(nutritionist);
            SaveChanges();
        }

        /// <summary>
        /// Verifica si un código de nutricionista es válido.
        /// </summary>
        /// <param name="code">Código a verificar.</param>
        /// <returns>True si el código existe; de lo contrario, false.</returns>
        public bool VerifyCode(int code)
        {
            return nutritionists.Any(n => n.Code == code);
        }

        /// <summary>
        /// Actualiza un nutricionista existente.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del nutricionista a actualizar.</param>
        /// <param name="updatedNutritionist">Nutricionista actualizado.</param>
        public void Update(string eIdentifier, Nutritionist updatedNutritionist)
        {
            var index = nutritionists.FindIndex(n => n.E_Identifier.Equals(eIdentifier, StringComparison.OrdinalIgnoreCase));
            if (index != -1)
            {
                nutritionists[index] = updatedNutritionist;
                SaveChanges();
            }
        }

        /// <summary>
        /// Verifica si un E_Identifier ya está en uso.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier a verificar.</param>
        /// <returns>True si ya está en uso; de lo contrario, false.</returns>
        public bool NutritionistExistsByIdentifier(string eIdentifier)
        {
            return nutritionists.Any(n => n.E_Identifier.Equals(eIdentifier, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Verifica si un E_Identifier ya está en uso, excluyendo un E_Identifier específico (para actualizaciones).
        /// </summary>
        /// <param name="eIdentifier">E_Identifier a verificar.</param>
        /// <param name="excludeIdentifier">E_Identifier a excluir de la verificación.</param>
        /// <returns>True si ya está en uso; de lo contrario, false.</returns>
        public bool NutritionistExistsByIdentifier(string eIdentifier, string excludeIdentifier)
        {
            return nutritionists.Any(n => n.E_Identifier.Equals(eIdentifier, StringComparison.OrdinalIgnoreCase)
                                      && !n.E_Identifier.Equals(excludeIdentifier, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Genera un código único de 6 dígitos.
        /// </summary>
        /// <returns>Código único.</returns>
        public int GenerateUniqueCode()
        {
            int newCode;
            var rand = new Random();
            do
            {
                newCode = rand.Next(100000, 999999); // Genera un código de 6 dígitos
            } while (nutritionists.Any(n => n.Code == newCode));
            return newCode;
        }

        /// <summary>
        /// Guarda los cambios en el archivo JSON.
        /// </summary>
        private void SaveChanges()
        {
            try
            {
                var json = JsonSerializer.Serialize(nutritionists, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));
                File.WriteAllText(jsonFilePath, json);
                Console.WriteLine("Cambios guardados en nutritionists.json.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                throw;
            }
        }
    }
}
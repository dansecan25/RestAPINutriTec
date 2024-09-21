using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NutriRestApi.Models;

namespace NutriRestApi.Services
{
    /// <summary>
    /// Servicio para manejar operaciones CRUD de Administradores.
    /// </summary>
    public class AdminService
    {
        private readonly string jsonFilePath = "Data/admins.json";
        private List<Admin> admins;

        /// <summary>
        /// Constructor que inicializa la lista de administradores desde un archivo JSON.
        /// </summary>
        public AdminService()
        {
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                admins = JsonSerializer.Deserialize<List<Admin>>(json);
            }
            else
            {
                admins = new List<Admin>();
                SaveChanges(); // Crear el archivo JSON si no existe
            }
        }

        /// <summary>
        /// Obtiene todos los administradores.
        /// </summary>
        /// <returns>Lista de administradores.</returns>
        public IEnumerable<Admin> GetAll()
        {
            return admins;
        }

        /// <summary>
        /// Obtiene un administrador por su E_Identifier.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del administrador.</param>
        /// <returns>Administrador si se encuentra; de lo contrario, null.</returns>
        public Admin GetByIdentifier(string eIdentifier)
        {
            return admins.Find(a => a.E_Identifier.Equals(eIdentifier, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Agrega un nuevo administrador.
        /// </summary>
        /// <param name="admin">Administrador a agregar.</param>
        public void Add(Admin admin)
        {
            admins.Add(admin);
            SaveChanges();
        }

        /// <summary>
        /// Actualiza un administrador existente.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del administrador a actualizar.</param>
        /// <param name="updatedAdmin">Administrador actualizado.</param>
        public void Update(string eIdentifier, Admin updatedAdmin)
        {
            var index = admins.FindIndex(a => a.E_Identifier.Equals(eIdentifier, System.StringComparison.OrdinalIgnoreCase));
            if (index != -1)
            {
                admins[index] = updatedAdmin;
                SaveChanges();
            }
        }
        

        /// <summary>
        /// Verifica si un administrador existe por su E_Identifier.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del administrador.</param>
        /// <returns>True si existe; de lo contrario, false.</returns>
        public bool AdminExistsByIdentifier(string eIdentifier)
        {
            return admins.Any(a => a.E_Identifier.Equals(eIdentifier, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Verifica si un E_Identifier ya está en uso, excluyendo un E_Identifier específico (para actualizaciones).
        /// </summary>
        /// <param name="eIdentifier">E_Identifier a verificar.</param>
        /// <param name="excludeIdentifier">E_Identifier a excluir de la verificación.</param>
        /// <returns>True si ya está en uso; de lo contrario, false.</returns>
        public bool AdminExistsByIdentifier(string eIdentifier, string excludeIdentifier)
        {
            return admins.Any(a => a.E_Identifier.Equals(eIdentifier, System.StringComparison.OrdinalIgnoreCase)
                                  && !a.E_Identifier.Equals(excludeIdentifier, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Guarda los cambios en el archivo JSON.
        /// </summary>
        private void SaveChanges()
        {
            try
            {
                var json = JsonSerializer.Serialize(admins, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));
                File.WriteAllText(jsonFilePath, json);
                Console.WriteLine("Cambios guardados en admins.json.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                throw;
            }
        }
    }
}
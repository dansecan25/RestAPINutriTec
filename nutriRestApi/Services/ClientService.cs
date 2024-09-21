using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NutriRestApi.Models;

namespace NutriRestApi.Services
{
    /// <summary>
    /// Servicio para manejar operaciones CRUD de Clientes.
    /// </summary>
    public class ClientService
    {
        private readonly string jsonFilePath = "Data/clients.json";
        private List<Client> clients;

        /// <summary>
        /// Constructor que inicializa la lista de clientes desde un archivo JSON.
        /// </summary>
        public ClientService()
        {
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                clients = JsonSerializer.Deserialize<List<Client>>(json);
            }
            else
            {
                clients = new List<Client>();
                SaveChanges(); // Crear el archivo JSON si no existe
            }
        }

        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        public IEnumerable<Client> GetAll()
        {
            return clients;
        }

        /// <summary>
        /// Obtiene un cliente por su E_Identifier.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del cliente.</param>
        /// <returns>Cliente si se encuentra; de lo contrario, null.</returns>
        public Client GetByIdentifier(string eIdentifier)
        {
            return clients.Find(c => c.E_Identifier.Equals(eIdentifier, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Agrega un nuevo cliente.
        /// </summary>
        /// <param name="client">Cliente a agregar.</param>
        public void Add(Client client)
        {
            clients.Add(client);
            SaveChanges();
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier del cliente a actualizar.</param>
        /// <param name="updatedClient">Cliente actualizado.</param>
        public void Update(string eIdentifier, Client updatedClient)
        {
            var index = clients.FindIndex(c => c.E_Identifier.Equals(eIdentifier, System.StringComparison.OrdinalIgnoreCase));
            if (index != -1)
            {
                clients[index] = updatedClient;
                SaveChanges();
            }
        }

        /// <summary>
        /// Verifica si un E_Identifier ya está en uso.
        /// </summary>
        /// <param name="eIdentifier">E_Identifier a verificar.</param>
        /// <returns>True si ya está en uso; de lo contrario, false.</returns>
        public bool ClientExistsByIdentifier(string eIdentifier)
        {
            return clients.Any(c => c.E_Identifier.Equals(eIdentifier, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Verifica si un E_Identifier ya está en uso, excluyendo un E_Identifier específico (para actualizaciones).
        /// </summary>
        /// <param name="eIdentifier">E_Identifier a verificar.</param>
        /// <param name="excludeIdentifier">E_Identifier a excluir de la verificación.</param>
        /// <returns>True si ya está en uso; de lo contrario, false.</returns>
        public bool ClientExistsByIdentifier(string eIdentifier, string excludeIdentifier)
        {
            return clients.Any(c => c.E_Identifier.Equals(eIdentifier, System.StringComparison.OrdinalIgnoreCase)
                                  && !c.E_Identifier.Equals(excludeIdentifier, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Guarda los cambios en el archivo JSON.
        /// </summary>
        private void SaveChanges()
        {
            try
            {
                var json = JsonSerializer.Serialize(clients, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));
                File.WriteAllText(jsonFilePath, json);
                Console.WriteLine("Cambios guardados en clients.json.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                throw;
            }
        }
    }
}
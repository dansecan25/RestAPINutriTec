using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NutriRestApi.Models;
using System.IO;

namespace NutriRestApi.Services
{
    public class UserService
    {
        private readonly string jsonFilePath = "Data/users.json";
        private List<User> users;

        public UserService()
        {
            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            else
            {
                users = new List<User>();
                SaveChanges(); // Crear el archivo JSON si no existe
            }
        }

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User GetById(int id)
        {
            return users.Find(u => u.Id == id);
        }

        public void Add(User user)
        {
            Console.WriteLine($"Agregando usuario con Id: {user.Id}");
            users.Add(user);
            SaveChanges();
            Console.WriteLine("Usuario agregado y cambios guardados.");
        }

        public void Update(int id, User updatedUser)
        {
            var user = GetById(id);
            if (user != null)
            {
                // Actualizar propiedades según sea necesario
                user.Age = updatedUser.Age;
                user.Password = updatedUser.Password;
                user.Birthdate = updatedUser.Birthdate;
                user.B_Day = updatedUser.B_Day;
                user.B_Month = updatedUser.B_Month;
                user.B_Year = updatedUser.B_Year;
                user.Email = updatedUser.Email;
                user.E_Identifier = updatedUser.E_Identifier;
                user.E_Domain = updatedUser.E_Domain;
                user.Fullname = updatedUser.Fullname;
                user.Name = updatedUser.Name;
                user.FirstlastName = updatedUser.FirstlastName;
                user.SecondlastName = updatedUser.SecondlastName;
                user.Username = updatedUser.Username;
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            try
            {
                var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath));
                File.WriteAllText(jsonFilePath, json);
                Console.WriteLine("Cambios guardados en users.json.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                throw;
            }
        }

        // Verificar si un Id ya existe
        public bool UserExistsById(int id)
        {
            return users.Any(u => u.Id == id);
        }

        // Verificar si un E_Identifier ya existe
        public bool UserExistsByIdentifier(string eIdentifier)
        {
            return users.Any(u => u.E_Identifier.Equals(eIdentifier, StringComparison.OrdinalIgnoreCase));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nutriRestApi.Models.Compuestos;

namespace nutriRestApi.Models
{
    public class Administrador : Usuario
    {
        public Administrador(int cedula, string tipoUsuario, string nombreUsuario,NombreCompleto nombreCompleto, Correo correo, FechaNacimiento fechaNacimiento, string contrasena)
        :base(cedula,tipoUsuario,nombreUsuario,nombreCompleto,correo,fechaNacimiento,contrasena){
            
        }
    }
}
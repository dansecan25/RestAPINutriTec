using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nutriRestApi.Models.Compuestos;

namespace nutriRestApi.Models
{
    public class Usuario
    {
        public int cedula { get; set; }

        public string tipoUsuario { get; set; }

        public string nombreUsuario { get; set; }

        public NombreCompleto nombreCompleto { get; set; }

        public Correo correo { get; set; }

        public FechaNacimiento fechaNacimiento {get; set;}

        public string contrasena { get; set; }

        public int edad 
        { get{
            DateTime fechaNac = fechaNacimiento.toDateTime();
            var hoy = DateTime.Today;
            var valor = hoy.Year - fechaNac.Year;
            //ajustar si aun no se pasa el cumple
            if(fechaNac.Date > hoy.AddYears(-valor)) valor--;
            return valor;
        } }

        public Usuario(int cedula, string tipoUsuario, string nombreUsuario, NombreCompleto nombreCompleto,Correo correo, FechaNacimiento fechaNacimiento, string contrasena)
        {
            this.cedula=cedula;
            this.tipoUsuario=tipoUsuario;
            this.nombreUsuario=nombreUsuario;
            this.nombreCompleto=nombreCompleto;
            this.correo=correo;
            this.fechaNacimiento=fechaNacimiento;
            this.contrasena=contrasena;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nutriRestApi.Models.Compuestos;

namespace nutriRestApi.Models
{
    public class Cliente : Usuario
    {
        public string pais { get; set; } //type prop and enter to create each

        public int pesoActual { get; set; } //kg

        public int indiceMasaCorporal { get; set; }

        public Medidas medidas { get; set; }

        public int porcentajeMusculo { get; set; }

        public int consumoDiarioMaximo { get; set; }

        public int porcentajeGrasa { get; set; }

        public Cliente(int cedula, string tipoUsuario, string nombreUsuario,NombreCompleto nombreCompleto, Correo correo, FechaNacimiento fechaNacimiento, string contrasena, string pais, int pesoActual, int indiceMasaCorporal, Medidas medidas, int porcentajeMusculo, int consumoDiarioMaximo, int porcentajeGrasa)
        :base(cedula,tipoUsuario,nombreUsuario,nombreCompleto,correo,fechaNacimiento,contrasena)
        {
            this.pais=pais;
            this.pesoActual=pesoActual;
            this.indiceMasaCorporal=indiceMasaCorporal;
            this.medidas=medidas;
            this.porcentajeMusculo=porcentajeMusculo;
            this.consumoDiarioMaximo=consumoDiarioMaximo;
            this.porcentajeGrasa=porcentajeGrasa;
        }
    }
}
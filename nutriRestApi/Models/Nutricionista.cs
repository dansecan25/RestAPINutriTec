using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nutriRestApi.Models.Compuestos;

namespace nutriRestApi.Models
{
    public class Nutricionista : Usuario
    {
        public int codigoNutricionisa { get; set; }

        public int peso { get; set; } //kg

        public int indiceMasaCorporal { get; set; }

        public string foto { get; set; }

        public TarjetaCobro tarjetaCobro { get; set; }

        public Direccion direccion {get; set;}

        public string tipoCobro { get; set; }

        public double cobroTotal { get; set; }

        public int descuento { get; set; }

        public double cobroFinal 
        { 
            get
            {
                double valorFinal=((descuento/100.0)*cobroTotal)-cobroFinal;
                return valorFinal;
            } }
        
        public Nutricionista(int cedula, string tipoUsuario, string nombreUsuario,NombreCompleto nombreCompleto, Correo correo, FechaNacimiento fechaNacimiento, string contrasena, int codigoNutricionisa, int peso, int indiceMasaCorporal, string foto, TarjetaCobro tarjetaCobro, Direccion direccion, string tipoCobro, double cobroTotal, int descuento)
        :base(cedula,tipoUsuario,nombreUsuario,nombreCompleto,correo,fechaNacimiento,contrasena){
            this.peso=peso;
            this.indiceMasaCorporal=indiceMasaCorporal;
            this.foto=foto;
            this.tarjetaCobro=tarjetaCobro;
            this.direccion=direccion;
            this.tipoCobro=tipoCobro;
            this.cobroTotal=cobroTotal;
            this.descuento=descuento;
        }
    }

        
}
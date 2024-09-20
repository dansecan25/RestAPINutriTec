using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nutriRestApi.Models.Compuestos;

namespace nutriRestApi.Models
{
    public class Nutricionista
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
        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models
{
    public class Platillo
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Vitaminas { get; set; }

        public int Proteina { get; set; }

        public int Hierro { get; set; }

        public int TamanoDePorcion { get; set; }

        public int Grasa { get; set; }

        public int Energia { get; set; }

        public int Sodio { get; set; }

        public int Calcio { get; set; }

        public int Carbohidratos { get; set; }

        public string Estado { get; set; }
    }
}
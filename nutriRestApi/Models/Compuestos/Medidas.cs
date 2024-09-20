using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace nutriRestApi.Models.Compuestos
{
    public class Medidas
    {
        public int cadera { get; set; }
        public int cuello { get; set; }
        public int cintura { get; set; }

        public Medidas(int cadera, int cuello, int cintura)
        {
            this.cadera=cadera;
            this.cuello=cuello;
            this.cintura=cintura;
        }
        public int getCadera()
        {
            return cadera;
        }

        public int getCuello()
        {
            return cuello;
        }

        public int getCintura()
        {
            return cintura;
        }
        
    }
}
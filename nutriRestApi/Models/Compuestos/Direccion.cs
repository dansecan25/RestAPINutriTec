using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models.Compuestos
{
    public class Direccion
    {
        public string provincia { get; set; }
        public string canton { get; set; }
        public string distrito { get; set; }

        public Direccion(string provincia, string canton, string distrito)
        {
            this.provincia=provincia;
            this.canton=canton;
            this.distrito=distrito;
        }
    }
}
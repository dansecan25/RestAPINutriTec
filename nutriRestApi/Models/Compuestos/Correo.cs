using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models.Compuestos
{
    public class Correo
    {
        public string dominio { get; set; }
        public string identificador { get; set; }

        // Constructor opcional
        public Correo(string dominio, string identificador)
        {
            this.dominio = dominio;
            this.identificador = identificador;
        }

        // Método para obtener el correo completo
        public override string ToString()
        {
            return $"{identificador}@{dominio}";
        }
    }

}
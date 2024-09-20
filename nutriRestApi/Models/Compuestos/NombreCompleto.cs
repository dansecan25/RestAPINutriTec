using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models.Compuestos
{
    public class NombreCompleto
    {
        public string? nombre { get; set; }
        public string? primerApellido { get; set; }
        public string? segundoApellido { get; set; }

        public NombreCompleto(string? nombre, string? primerApellido, string? segundoApellido)
        {
            this.nombre=nombre;
            this.primerApellido=primerApellido;    
            this.segundoApellido=segundoApellido;
        }

        public override string ToString()
        {
            return $"{nombre} {primerApellido} {segundoApellido}";
        }
    }
}
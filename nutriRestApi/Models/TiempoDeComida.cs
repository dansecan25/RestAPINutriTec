using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models
{
    public class TiempoDeComida
    {
        public int idTiempoDeComida {get; set;}

        public string tipo {get; set;}

        public TiempoDeComida(int idTiempoDeComida, string tipo){
            this.idTiempoDeComida=idTiempoDeComida;
            this.tipo=tipo;
        }
    }
}
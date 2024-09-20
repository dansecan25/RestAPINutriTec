using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models
{
    public class ConsumoDiario
    {
        public List<string> platillosConsumidos {get; set;}
        
        public List<string> productosConsumidos {get; set;}

        public ConsumoDiario(List<string> platillosConsumidos, List<string> productosConsumidos){
            this.platillosConsumidos=platillosConsumidos;
            this.productosConsumidos=productosConsumidos;
        }
    }
}
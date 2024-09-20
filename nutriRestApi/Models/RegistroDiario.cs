using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nutriRestApi.Models.Compuestos;

namespace nutriRestApi.Models
{
    public class RegistroDiario
    {
        public Fecha fecha {get; set;}

        public string retroalimentacion {get; set;}

        public Medidas medidas {get; set;}

        public RegistroDiario(Fecha fecha, string retroalimentacion, Medidas medidas){
            this.fecha=fecha;
            this.medidas=medidas;
            this.retroalimentacion=retroalimentacion;
        }

    }
}
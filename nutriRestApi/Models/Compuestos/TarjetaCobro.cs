using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models.Compuestos
{
    public class TarjetaCobro
    {
        public string nombreTarjeta { get; set; }
        public int numeroTarjeta { get; set; }
        public int cvcTarjeta { get; set; }
        public FechaVencimientoTarjeta fechaVencimientoTarjeta { get; set; }

        public TarjetaCobro(string nombreTarjeta, int numero, int cvc, int dia, int mes, int ano )
        {
            this.nombreTarjeta=nombreTarjeta;
            this.numeroTarjeta=numero;
            this.cvcTarjeta=cvc;
            this.fechaVencimientoTarjeta=new FechaVencimientoTarjeta(dia,mes,ano);
        }
    }
}
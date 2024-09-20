using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models.Compuestos
{
    public class FechaVencimientoTarjeta
    {
        public int mes { get; set; }
        public int dia { get; set; }
        public int ano { get; set; }

        public FechaVencimientoTarjeta(int dia, int mes, int ano)
        {
            this.dia=dia;
            this.mes=mes;
            this.ano=ano;
        }

        public DateTime toDateTime()
        {
            return new DateTime(dia, mes, ano);
        }
    }
}
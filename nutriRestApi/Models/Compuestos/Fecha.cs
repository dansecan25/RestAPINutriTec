using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models.Compuestos
{
    public class Fecha
    {
        public int dia { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }

        public Fecha(int dia, int mes, int ano)
        {
            this.dia=dia;
            this.mes=mes;
            this.ano=ano;
        }

        public override string ToString()
        {
        return $"{dia}/{mes}/{ano}";
        }

        public DateTime toDateTime()
        {
            return new DateTime(dia, mes, ano);
        }
    }
}
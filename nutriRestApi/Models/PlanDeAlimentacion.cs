using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using nutriRestApi.Models.Compuestos;

namespace nutriRestApi.Models
{
    public class PlanDeAlimentacion
    {
        public Fecha fecha {get; set;}

        public string nombre {get; set;}

        public int idPlan {get; set;}

        public string nombreNutricionista {get; set;}

        public int totalizacionCalorias
        { get{
            return 0; //NEED FIX: AQUI VA EL CALCULO TOTAL DE CALORIAS EN BASE A LOS PLATOS ALMACENADOS EN EL TIEMPO DE COMIDA
        } }

        public PlanDeAlimentacion(Fecha fecha, string nombre, int idPlan, string nombreNutricionista){
            this.fecha=fecha;
            this.nombre=nombre;
            this.idPlan=idPlan;
            this.nombreNutricionista=nombreNutricionista;
        }
    }
}
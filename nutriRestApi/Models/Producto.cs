using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nutriRestApi.Models
{
    public class Producto
    {
        public int hierro {get; set;}

        public List<string> vitaminas {get; set;}

        public int proteina {get; set;}

        public string estado {get; set;}

        public int carbohidratos {get; set;}

        public int calcio {get; set;}

        public int sodio {get; set;}

        public int grasa {get; set;}

        public int energia {get; set;}

        public int tamañoPorcion {get; set;}
        
        public string descripcion {get; set;}

        public double codigoDeBarras {get; set;}

        public Producto(int hierro, List<string> vitaminas, int proteina, string estado, int carbohidratos, int calcio, int sodio, int grasa, int energia, int tamañoPorcion, string descripcion, double codigoDeBarras){
            this.hierro=hierro;
            this.vitaminas=vitaminas;
            this.proteina=proteina;
            this.estado=estado;
            this.carbohidratos=carbohidratos;
            this.calcio=calcio;
            this.sodio=sodio;
            this.grasa=grasa;
            this.energia=energia;
            this.tamañoPorcion=tamañoPorcion;
            this.descripcion=descripcion;
            this.codigoDeBarras=codigoDeBarras;
        }
    }
}
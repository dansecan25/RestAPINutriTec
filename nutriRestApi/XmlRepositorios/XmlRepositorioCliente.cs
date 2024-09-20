using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using nutriRestApi.Models;
using nutriRestApi.Models.Compuestos;
#pragma warning disable CS8604
#pragma warning disable CS8602
#pragma warning disable CS8603
#pragma warning disable CS8600
namespace nutriRestApi.XmlRepositorios
{
    public class XmlRepositorioCliente
    {
        private readonly string rutaXml;

        public XmlRepositorioCliente(string ruta)
        {
            this.rutaXml=ruta;
        }

        public void PostCliente(Cliente cliente)
        {
            var doc = XDocument.Load(rutaXml);
            var root = doc.Root;

            //Verifica si existe el cliente
            var clienteNuevo = root.Elements("Cliente").FirstOrDefault(e => (int)e.Element("Cedula") == cliente.cedula);

            if (clienteNuevo != null)
            {
                clienteNuevo.Remove();
            }

            // Agregar el nuevo cliente
            var clienteElement = new XElement("Cliente",
                new XElement("Cedula", cliente.cedula),
                new XElement("TipoUsuario", cliente.tipoUsuario),
                new XElement("NombreUsuario", cliente.nombreUsuario),
                new XElement("NombreCompleto",
                    new XElement("Nombre", cliente.nombreCompleto.nombre),
                    new XElement("PrimerApellido", cliente.nombreCompleto.primerApellido),
                    new XElement("SegundoApellido", cliente.nombreCompleto.segundoApellido)),
                new XElement("Correo",
                    new XElement("Dominio", cliente.correo.dominio),
                    new XElement("Identificador",cliente.correo.identificador)),
                new XElement("FechaNacimiento",
                    new XElement("Dia", cliente.fechaNacimiento.dia),
                    new XElement("Mes", cliente.fechaNacimiento.mes),
                    new XElement("Año", cliente.fechaNacimiento.ano)),
                new XElement("Contrasena", cliente.contrasena),
                new XElement("Pais",cliente.pais),
                new XElement("PesoActual", cliente.pesoActual),
                new XElement("IndiceMasaCorporal", cliente.indiceMasaCorporal),
                new XElement("Medidas",
                    new XElement("cadera", cliente.medidas.cadera),
                    new XElement("cuello", cliente.medidas.cuello),
                    new XElement("cintura",cliente.medidas.cintura)),
                new XElement("PorcentajeMusculo", cliente.porcentajeMusculo),
                new XElement("ConsumoDiarioMaximo", cliente.consumoDiarioMaximo),
                new XElement("PorcentajeGrasa", cliente.porcentajeGrasa)
            );

            root.Add(clienteElement); 
            doc.Save(rutaXml);//guarda los cambios en el xml
        }
        
        public List<Cliente> GetAllClientes()
        {
             var archivo = XDocument.Load(rutaXml);
             var listaclientes = new List<Cliente>();

            foreach (var clienteElement in archivo.Root.Elements("Cliente")) //itera por cada uno de los clientes en el xml
            {
                // Reutiliza la lógica de mapeo usada en GetCliente
                int ced = (int)clienteElement.Element("Cedula");
                string tipoUsuario = (string)clienteElement.Element("TipoUsuario");
                string nombreUsuario = (string)clienteElement.Element("NombreUsuario");
                string nombre = (string)clienteElement.Element("NombreCompleto").Element("Nombre");
                string primerApellido = (string)clienteElement.Element("NombreCompleto").Element("PrimerApellido");
                string segundoApellido = (string)clienteElement.Element("NombreCompleto").Element("SegundoApellido");
                NombreCompleto nombreCompleto = new NombreCompleto(nombre, primerApellido, segundoApellido);
                string dominio = (string)clienteElement.Element("Correo").Element("Dominio");
                string identificador = (string)clienteElement.Element("Correo").Element("Identificador");
                Correo correo = new Correo(dominio, identificador);
                int dia = (int)clienteElement.Element("FechaNacimiento").Element("Dia");
                int mes = (int)clienteElement.Element("FechaNacimiento").Element("Mes");
                int ano = (int)clienteElement.Element("FechaNacimiento").Element("Año");
                FechaNacimiento fecha = new FechaNacimiento(dia, mes, ano);
                string contrasena = (string)clienteElement.Element("Contrasena");
                string pais = (string)clienteElement.Element("Pais");
                int pesoActual = (int)clienteElement.Element("PesoActual");
                int indiceMasaCorporal = (int)clienteElement.Element("IndiceMasaCorporal");
                int cadera = (int)clienteElement.Element("Medidas").Element("cadera");
                int cuello = (int)clienteElement.Element("Medidas").Element("cuello");
                int cintura = (int)clienteElement.Element("Medidas").Element("cintura");
                Medidas medidas = new Medidas(cadera, cuello, cintura);
                int porcentajeMusculo = (int)clienteElement.Element("PorcentajeMusculo");
                int consumoDiarioMaximo = (int)clienteElement.Element("ConsumoDiarioMaximo");
                int porcentajeGrasa = (int)clienteElement.Element("PorcentajeGrasa");

                // Crea la instancia de Cliente y agrégala a la lista
                Cliente cliente = new Cliente(
                    ced,
                    tipoUsuario,
                    nombreUsuario,
                    nombreCompleto,
                    correo,
                    fecha,
                    contrasena,
                    pais,
                    pesoActual,
                    indiceMasaCorporal,
                    medidas,
                    porcentajeMusculo,
                    consumoDiarioMaximo,
                    porcentajeGrasa
                );

                listaclientes.Add(cliente);
            }


             return listaclientes;
        }
        public Cliente GetCliente(int cedula) 
        {
            var archivo = XDocument.Load(rutaXml);
            //busca un cliente con base al numero de cedula

            var clienteElement = archivo.Root.Elements("Cliente").FirstOrDefault(
                e => (int)e.Element("Cedula") == cedula);

            if (clienteElement == null)
            {
                return null;
            }
            else
            {   
                //Obtiene primero todos los datos del xml
                //cedula
                int ced = (int)clienteElement.Element("Cedula");

                //tipo de Usuario
                string tipoUsuario = (string)clienteElement.Element("TipoUsuario");

                //nombre de usuario
                string nombreUsuario= (string)clienteElement.Element("NombreUsuario");

                //Nombre completo
                string nombre=(string)clienteElement.Element("NombreCompleto").Element("Nombre");
                string primerApellido= (string)clienteElement.Element("NombreCompleto").Element("PrimerApellido");
                string segundoApellido=  (string)clienteElement.Element("NombreCompleto").Element("SegundoApellido");
                NombreCompleto nombreCompleto= new NombreCompleto(nombre, primerApellido, segundoApellido);
                
                //Correo
                string dominio=(string)clienteElement.Element("Correo").Element("Dominio");
                string identificador=(string)clienteElement.Element("Correo").Element("Identificador");
                Correo correo= new Correo(dominio,identificador);

                //fecha de nacimiento
                int dia= (int)clienteElement.Element("FechaNacimiento").Element("Dia");
                int mes= (int)clienteElement.Element("FechaNacimiento").Element("Mes");
                int ano= (int)clienteElement.Element("FechaNacimiento").Element("Año");
                FechaNacimiento fecha= new FechaNacimiento(dia,mes,ano);

                //contraseña
                string contrasena = (string)clienteElement.Element("Contrasena");

                //pais
                string pais= (string)clienteElement.Element("Pais");

                //peso actual en kg
                int pesoActual = (int)clienteElement.Element("PesoActual");

                //IMC
                int indiceMasaCorporal = (int)clienteElement.Element("IndiceMasaCorporal");
                
                //Medidas
                int cadera = (int)clienteElement.Element("Medidas").Element("cadera");
                int cuello = (int)clienteElement.Element("Medidas").Element("cuello");
                int cintura = (int)clienteElement.Element("Medidas").Element("cintura");
                Medidas medidas = new Medidas(cadera, cuello, cintura);
                
                //porcentaje de musculo
                int porcentajeMusculo = (int)clienteElement.Element("PorcentajeMusculo");
                
                //consumo diario maximo
                int consumoDiarioMaximo = (int)clienteElement.Element("ConsumoDiarioMaximo");

                //porcentaje de grasa

                int porcentajeGrasa = (int)clienteElement.Element("PorcentajeGrasa");

                return new Cliente(
                    ced,
                    tipoUsuario,
                    nombreUsuario,
                    nombreCompleto,
                    correo,
                    fecha,
                    contrasena,
                    pais,
                    pesoActual,
                    indiceMasaCorporal,
                    medidas,
                    porcentajeMusculo,
                    consumoDiarioMaximo,
                    porcentajeGrasa);

            }
        }

        public void UpdateCliente(int cedula, string valorActualizar,params object[] valores)
        {

        }
        public void DeleteCliente(int cedula, string valorBorrar, params object[] valores)
        {

        }
    }
}
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603
#pragma warning restore CS8602
#pragma warning restore CS8600
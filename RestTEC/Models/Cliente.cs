using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestTEC.Models
{
    public class Cliente
    {
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }        
        public string UserName { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public int DiaNacimiento { get; set; }
        public int MesNacimiento { get; set; }
        public int AnioNacimiento { get; set; }
        public int Telefono { get; set; }        
        public int[] OrdenesActuales { get; set; }
        public int[] HistorialOrdenesRealizadas { get; set; }

    }
    public class ClienteBL
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "clientes.json"));
        private List<Cliente> DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            Cliente[] clients = JsonConvert.DeserializeObject<Cliente[]>(jsonString);

            List<Cliente> clientsList = clients.ToList();
            /* --------------------------------- SourceData Method -----------------------------------*/

            return clientsList;
        }
        public IEnumerable<Cliente> GetAll()
        {
            //(Read) GET            
            return DataSource();
        }
        public Cliente Insert(Cliente newClient)
        {
            //(Created) POST                  
            List<Cliente> clientsList = DataSource(); // Relacion Client en su estado actual deserealizada

            bool existedClient = clientsList.Any(client => client.UserName.Equals(newClient.UserName));

            if (existedClient)
            {
                return null;
            }
            else
            {
                string cedula = newClient.Cedula.ToString();
                int diaNacimiento = newClient.DiaNacimiento;
                int mesNacimiento = newClient.MesNacimiento;
                string anioNacimiento = newClient.AnioNacimiento.ToString();
                string telefono = newClient.Telefono.ToString();

                if( (cedula.Length == 9)   && 
                    (telefono.Length == 8) && 
                    (1 <= diaNacimiento  && diaNacimiento <= 31)   && 
                    (1 <= mesNacimiento  && mesNacimiento <= 12)   && 
                    (anioNacimiento.Length == 4))
                {
                clientsList.Add(newClient); //Agregamos al cliente
                Serialize(clientsList); //Almacenamos la ultima version de la relacion Client

                return newClient;
                }
                else
                {
                    return null;
                }

            }
            
        }
        public Cliente InsertPedidoAClienteByOrderNumber(int numeroOrden, string UserName)
        {
            Cliente actualClient = GetById(UserName);

            if (actualClient == null)
            {
                return null;
            }
            else
            {
                if (actualClient.HistorialOrdenesRealizadas == null)
                {
                    actualClient.HistorialOrdenesRealizadas = new int[] { numeroOrden };
                }
                else
                {
                    var HistorialPedidos = actualClient.HistorialOrdenesRealizadas.ToList();
                    HistorialPedidos.Add(numeroOrden);
                    actualClient.HistorialOrdenesRealizadas = HistorialPedidos.ToArray();
                }


                if (actualClient.OrdenesActuales == null)
                {
                    actualClient.OrdenesActuales = new int[] { numeroOrden };
                }
                else
                {
                    var ordenesActuales = actualClient.OrdenesActuales.ToList();
                    ordenesActuales.Add(numeroOrden);
                    actualClient.OrdenesActuales = ordenesActuales.ToArray();
                }
                                

                Update(actualClient);
                return actualClient;
            }
            
        }
        public int RemoveActualPedido(int numeroOrden)
        {
            List<Cliente> clients = DataSource();

            for(int i = 0; i < clients.Count; i++)
            {
                if ( clients[i].OrdenesActuales != null || clients[i].OrdenesActuales.Length != 0)
                {
                    if(clients[i].OrdenesActuales.ToList().Any(orden => orden == numeroOrden))
                    {
                        var listaOrdenes = clients[i].OrdenesActuales.ToList();
                        listaOrdenes.Remove(numeroOrden);
                        clients[i].OrdenesActuales = listaOrdenes.ToArray();
                        break;

                    }
                }
            }

            Serialize(clients);

            return numeroOrden;
        }
        public Cliente GetById(string clientUserName) 
        {
            //(Read) GET         
            Cliente client = DataSource().FirstOrDefault(user => user.UserName.Equals(clientUserName));
            
            if(client == null)
            {
                return null;
            }
            return client;
        }
        public Cliente Update(Cliente actualClient)    
        {
            //(Update) PUT            
            var oldClient = Delete(actualClient.UserName);
            if (oldClient != null) //Seria diferente de null SOLO SI oldClient si existe
            {
                actualClient = Insert(actualClient);
                if(actualClient != null)
                {
                    
                    return oldClient;
                }
                else
                {
                    Insert(oldClient);
                    return null;
                }
            }
            else
            {                
                return null;
            }
            
        }
        public Cliente Delete(string clientUserName)  
        {
            //(Delete) DELETE
            
            List<Cliente> clientsList = DataSource();// Relacion Client en su estado actual deserealizada

            Cliente client = clientsList.SingleOrDefault(user => user.UserName.Equals(clientUserName));
            if (client == null)
            {
                return null; //Si el client que se desea borrar no existe, se retorna un null
            }

            clientsList.Remove(client);
            Serialize(clientsList); //Almacenamos la ultima version de la relacion Client

            return client; //Se retorna el client como convension para que se sepa que el mismo si existia en la base de datos

        }
        private void Serialize(List<Cliente> clientsList)
        {
            /* ------------------- Serialize Method -----------------------*/
            //studentsList.ToArray();
            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, clientsList);
            }
            /* ------------------- Serialize Method -----------------------*/
        }
    }
    public class ClientRegistration
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DiaNacimiento { get; set; }
        public int MesNacimiento { get; set; }
        public int AnioNacimiento { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public int Telefono { get; set; }
    }
}
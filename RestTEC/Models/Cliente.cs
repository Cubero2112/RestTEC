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
        public int Telefono { get; set; }
        public int Orden { get; set; }
        public Pedido[] HistorialPedidos { get; set; }

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
        public Cliente Insert(Cliente client)        
        {
            //(Created) POST             
            List<Cliente> clientsList = DataSource(); // Relacion Client en su estado actual deserealizada
            clientsList.Add(client);
            Serialize(clientsList); //Almacenamos la ultima version de la relacion Client

            return client;
            
        }
        public IEnumerable<Cliente> GetAll()      
        {
            //(Read) GET            
            return DataSource();
        }
        public Cliente GetById(int CedulaCliente) 
        {
            //(Read) GET            
            Cliente client = DataSource().FirstOrDefault(singleClient => singleClient.Cedula == CedulaCliente);
            if(client == null)
            {
                return null;
            }
            return client;
        }
        public Cliente Update(Cliente cliente)    
        {
            //(Update) PUT            
            var localStudent = Delete(cliente.Cedula);
            if (localStudent != null)
            {
                Insert(cliente);
                return cliente;
            }
            return null;
        }
        public Cliente Delete(int CedulaCliente)  
        {
            //(Delete) DELETE
            
            List<Cliente> clientsList = DataSource();// Relacion Client en su estado actual deserealizada

            Cliente client = clientsList.SingleOrDefault(singleClient => singleClient.Cedula == CedulaCliente);
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
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public int Telefono { get; set; }
    }
}
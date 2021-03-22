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
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public int DiaNacimiento { get; set; }
        public int MesNacimiento { get; set; }
        public int Telefono { get; set; }
        public int Orden { get; set; }
        public Platillo[] HistorialPedidos { get; set; }

    }

    public class ClienteLogic
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "clientes.json"));
        public void Insert(Cliente client)        //(C) POST 
        {
            //logic to insert a new client
            /* ------------------- Post/insert Method -----------------------*/
            List<Cliente> clientsList = DataSource(); // Base de datos actual deserealizada
            clientsList.Add(client);
            Serialize(clientsList); //Almacenamos la ultima version de la base de datos
            /* ------------------- Post/insert Method -----------------------*/
        }
        public IEnumerable<Cliente> GetAll()      //(R) GET
        {
            //logic to return all clients
            return DataSource();
        }
        public Cliente GetById(int CedulaCliente) //(R) GET
        {
            //logic to return a client by clientId(Cedula)
            Cliente client = DataSource().FirstOrDefault(singleClient => singleClient.Cedula == CedulaCliente);
            return client;
        }
        public Cliente Update(Cliente cliente)    //(U) PUT
        {
            //logic to Update a client
            var localStudent = Delete(cliente.Cedula);
            if (localStudent != null)
            {
                Insert(cliente);
                return cliente;
            }
            return null;
        }
        public Cliente Delete(int CedulaCliente)  //(D) DELETE
        {
            //logic to Delete a client
            List<Cliente> clientsList = DataSource(); // Base de datos actual

            Cliente client = clientsList.SingleOrDefault(singleClient => singleClient.Cedula == CedulaCliente);
            if (client == null)
            {
                return null; //Si el client que se desea borrar no existia, se retorna un null
            }

            clientsList.Remove(client);
            Serialize(clientsList); //Almacenamos la ultima version de la base de datos

            return client; //Se retorna el student como convension para que se sepa que el mismo si existia en la base de datos

        }
        private List<Cliente> DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            //Console.WriteLine(jsonString);

            Cliente[] clients = JsonConvert.DeserializeObject<Cliente[]>(jsonString);

            List<Cliente> clientsList = clients.ToList();
            /* --------------------------------- SourceData Method -----------------------------------*/

            return clientsList;
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
}
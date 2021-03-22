using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestTEC.Models
{
    public class Pedido
    {
        public string Chef { get; set; }
        public Platillo[] Platillos { get; set; }
        public double TiempoPreparacion { get; set; }
        public bool Estado { get; set; }
        public int Orden { get; set; }
    }

    public class PedidoLogic
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "pedidos.json"));
        public void Insert(Pedido pedido)   //(C) POST 
        {
            //logic to insert an student
            /* ------------------- Post/insert Method -----------------------*/
            List<Pedido> pedidosList = DataSource(); // Base de datos actual deserealizada
            pedidosList.Add(pedido);
            Serialize(pedidosList); //Almacenamos la ultima version de la base de datos
            /* ------------------- Post/insert Method -----------------------*/
        }
        public IEnumerable<Pedido> GetAll() //(R) GET
        {
            //logic to return all employees
            return DataSource();
        }
        public Pedido GetById(int Orden)    //(R) GET
        {
            //logic to return a client by clientId(Cedula)
            Pedido pedido = DataSource().FirstOrDefault(singlePedido => singlePedido.Orden == Orden);
            return pedido;
        }
        public Pedido Update(Pedido pedido) //(U) PUT
        {
            //logic to Update a pedido
            var localPedido = Delete(pedido.Orden);
            if (localPedido != null)
            {
                Insert(localPedido);
                return localPedido;
            }
            return null;
        }
        public Pedido Delete(int Orden)     //(D) DELETE
        {
            //logic to Delete 
            List<Pedido> pedidosList = DataSource(); // Base de datos actual

            Pedido pedido = pedidosList.SingleOrDefault(singlePedido => singlePedido.Orden == Orden);
            if (pedido == null)
            {
                return null; //Si el student que se desea borrar no existia, se retorna un null
            }

            pedidosList.Remove(pedido);
            Serialize(pedidosList); //Almacenamos la ultima version de la base de datos

            return pedido; //Se retorna el student como convension para que se sepa que el mismo si existia en la base de datos

        }
        private List<Pedido> DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            //Console.WriteLine(jsonString);

            Pedido[] pedidos = JsonConvert.DeserializeObject<Pedido[]>(jsonString);

            List<Pedido> clientsList = pedidos.ToList();
            /* --------------------------------- SourceData Method -----------------------------------*/

            return clientsList;
        }
        private void Serialize(List<Pedido> pedidosList)
        {

            /* ------------------- Serialize Method -----------------------*/
            //studentsList.ToArray();
            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, pedidosList);
            }
            /* ------------------- Serialize Method -----------------------*/
        }
    }
}
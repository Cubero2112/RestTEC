using Newtonsoft.Json;
using RestTEC.Authentication;
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
        public double TiempoPreparacionPromedio { get; set; }
        public bool EstaListo { get; set; }
        public int Orden { get; set; }
        public string Init { get; set; }
        public string Finish { get; set; }
        public string TiempoPreparacionReal { get; set; }
    }
    public class PedidoLogic
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "pedidos.json"));
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
        public IEnumerable<Pedido> GetAll() 
        {
            //(R) GET
            return DataSource();
        }
        public Pedido GetByOrderNumber(int Orden)
        {
            //(R) GET            
            Pedido pedido = DataSource().FirstOrDefault(singlePedido => singlePedido.Orden == Orden);
            if(pedido == null)
            {
                return null;
            }
            else
            {
                return pedido;
            }
        }
        public List<Pedido> GetPedidosNoAsignados()
        {
            List<Pedido> pedidos = DataSource();
            List<Pedido> pedidosNoAsignados = new List<Pedido>();

            foreach (Pedido pedido in pedidos)
            {
                if(pedido.Chef.Equals("No asignado"))
                {
                    pedidosNoAsignados.Add(pedido);
                }
            }

            return pedidosNoAsignados;
        }
        public List<Pedido> GetChefPedidos()
        {
            string Username = BasicAuthenticationAttribute.UserNameActual;

            UserBL userBL = new UserBL();
            User chef = userBL.GetByUserName(Username);

            if(chef == null)
            {
                return null;
            }
            else
            {

                List<Pedido> pedidos = DataSource();
                List<Pedido> pedidosAsignados = new List<Pedido>();

                foreach (Pedido pedido in pedidos)
                {
                    if (pedido.Chef.Equals(Username))
                    {
                        pedidosAsignados.Add(pedido);
                    }
                }

                return pedidosAsignados;
            }
        }
        public Pedido Insert(Pedido pedido)
        {
            //(C) POST 
            pedido = FillPedido(pedido);

            if(pedido.Platillos != null)
            {
                List<Pedido> pedidosList = DataSource(); // Base de datos actual deserealizada

                int numeroOrden = ConteoBL.AumentarPedidos();
                pedido.Orden = numeroOrden;

                pedidosList.Add(pedido);
                
                Serialize(pedidosList); //Almacenamos la ultima version de la base de datos

                return pedido;

            }
            else
            {
                return null;
            }            
        }
        private Pedido Update(Pedido pedido)
        {
            //(U) PUT                                                      
            List<Pedido> pedidosList = DataSource(); // Base de datos actual

            Pedido pedidoARemplazar = pedidosList.SingleOrDefault(singlePedido => singlePedido.Orden == pedido.Orden);

            int indexOfPedidoARemplazar = pedidosList.IndexOf(pedidoARemplazar);

            if(indexOfPedidoARemplazar != -1)
            {
                pedidosList[indexOfPedidoARemplazar] = pedido;
                Serialize(pedidosList);
                return pedidoARemplazar;
            }
            else
            {
                return null;
            }        
        }
        public Pedido Delete(int Orden)
        {
            //(D) DELETE

            List<Pedido> pedidosList = DataSource(); // Base de datos actual

            Pedido pedido = pedidosList.SingleOrDefault(singlePedido => singlePedido.Orden == Orden);
            if (pedido == null)
            {
                return null; //Si el student que se desea borrar no existia, se retorna un null
            }

            ConteoBL conteoBL = new ConteoBL();
            //int numeroPedidos = conteoBL.DisminuirPedidos();

            pedidosList.Remove(pedido);
            Serialize(pedidosList); //Almacenamos la ultima version de la base de datos

            return pedido; //Se retorna el student como convension para que se sepa que el mismo si existia en la base de datos

        }
        public Pedido InitPedido(int Orden)
        {
            Pedido pedido = GetByOrderNumber(Orden);
            UserBL userBL = new UserBL();

            string ChefUserName = BasicAuthenticationAttribute.UserNameActual;
            User chef = userBL.GetByUserName(ChefUserName);

            if(pedido == null || chef == null)
            {
                return null;
            }
            else
            {
                if (pedido.Chef.Equals("No asignado"))
                {
                    pedido.Init = DateTime.Now.ToString("HH':'mm':'ss");
                    pedido.Chef = ChefUserName;
                    Update(pedido);

                    return pedido;
                }
                else
                {
                    return null;
                }
            }
        }
        public Pedido FinishPedido(int Orden)
        {
            Pedido pedido = GetByOrderNumber(Orden);
            UserBL userBL = new UserBL();

            string ChefUserName = BasicAuthenticationAttribute.UserNameActual;
            User chef = userBL.GetByUserName(ChefUserName);

            if (pedido == null || chef == null || !chef.UserName.Equals(pedido.Chef))
            {
                return null;
            }
            else
            {
                pedido.EstaListo = true;
                pedido.Finish = DateTime.Now.ToString("HH':'mm':'ss");
                pedido.TiempoPreparacionReal = DateTime.Parse(pedido.Finish).Subtract(DateTime.Parse(pedido.Init)).ToString();
                Update(pedido);

                return pedido;
            }
        }
        private Pedido FillPedido(Pedido pedido)
        {
            if(pedido.Platillos != null)
            {
                PlatilloLogic platilloLogic = new PlatilloLogic();

                pedido.Chef = "No asignado";

                double tiempoTotal = 0.0;
                for (int i = 0; i < pedido.Platillos.Length; i++)
                {

                    pedido.Platillos[i] = platilloLogic.GetByCodigo(pedido.Platillos[i].Codigo);
                    platilloLogic.UpdateNumeroVentas(pedido.Platillos[i].Codigo);
                    tiempoTotal += pedido.Platillos[i].TiempoPreparacion;
                }

                pedido.TiempoPreparacionPromedio = tiempoTotal;

                pedido.EstaListo = false;

                return pedido;
            }
            else
            {
                return null;
            }
        }
        private void Serialize(List<Pedido> pedidosList)
        {

            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, pedidosList);
            }
            
        }
    }
}
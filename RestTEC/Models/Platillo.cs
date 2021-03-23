using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestTEC.Models
{
    public class Platillo
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public double Calorias { get; set; }
        public string Tipo { get; set; }
        public double Puntuacion { get; set; }
        public int NumeroVentas { get; set; }
        public double TiempoPreparacion { get; set; }
        public string Feedback { get; set; }
    }

    public class PlatilloLogic
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "platillos.json"));
        public void Insert(Platillo platillo)           //(C) POST 
        {
            //logic to insert an student
            /* ------------------- Post/insert Method -----------------------*/
            List<Platillo> platilloList = DataSource(); // Base de datos actual deserealizada
            platilloList.Add(platillo);
            Serialize(platilloList); //Almacenamos la ultima version de la base de datos
            /* ------------------- Post/insert Method -----------------------*/
        }
        public IEnumerable<Platillo> GetAll()           //(R) GET
        {
            //logic to return all employees
            return DataSource();
        }
        public Platillo GetById(int Codigo)             //(R) GET
        {
            //logic to return a client by clientId(Cedula)
            Platillo platillo = DataSource().FirstOrDefault(singlePlatillo => singlePlatillo.Codigo == Codigo);
            return platillo;
        }
        public Platillo Update(Platillo actualPlatillo) //(U) PUT
        {
            //logic to Update a pedido
            var oldPlatillo = Delete(actualPlatillo.Codigo);
            if (oldPlatillo != null)
            {
                Insert(actualPlatillo);
                return oldPlatillo;
            }
            return null;
        }
        public Platillo Delete(int Codigo)              //(D) DELETE
        {
            //logic to Delete 
            List<Platillo> pedidosList = DataSource(); // Base de datos actual

            Platillo platillo = pedidosList.SingleOrDefault(singlePlatillo => singlePlatillo.Codigo == Codigo);
            if (platillo == null)
            {
                return null; //Si el student que se desea borrar no existia, se retorna un null
            }

            pedidosList.Remove(platillo);
            Serialize(pedidosList); //Almacenamos la ultima version de la base de datos

            return platillo; //Se retorna el student como convension para que se sepa que el mismo si existia en la base de datos

        }
        private List<Platillo> DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            //Console.WriteLine(jsonString);

            Platillo[] platillos = JsonConvert.DeserializeObject<Platillo[]>(jsonString);

            List<Platillo> platillosList = platillos.ToList();
            /* --------------------------------- SourceData Method -----------------------------------*/

            return platillosList;
        }
        private void Serialize(List<Platillo> platillosList)
        {

            /* ------------------- Serialize Method -----------------------*/
            //studentsList.ToArray();
            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, platillosList);
            }
            /* ------------------- Serialize Method -----------------------*/
        }
    }
}
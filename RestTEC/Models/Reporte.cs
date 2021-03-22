using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestTEC.Models
{
    public class Reporte
    {
        public int Id { get; set; }
        public Platillo PlatilloMasVendido { get; set; }
        public Platillo PlatilloMasComprado { get; set; }
        public Platillo PlatilloMejorFeedBack { get; set; }
        public Cliente[] ClientesMasFieles { get; set; }


        public class ReporteLogic
        {
            private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "reportes.json"));
            public void Insert(Reporte reporte)   //(C) POST 
            {
                //logic to insert an student
                /* ------------------- Post/insert Method -----------------------*/
                List<Reporte> pedidosList = DataSource(); // Base de datos actual deserealizada
                pedidosList.Add(reporte);
                Serialize(pedidosList); //Almacenamos la ultima version de la base de datos
                /* ------------------- Post/insert Method -----------------------*/
            }
            public IEnumerable<Reporte> GetAll() //(R) GET
            {
                //logic to return all employees
                return DataSource();
            }
            public Reporte GetById(int ReporteID)    //(R) GET
            {
                //logic to return a client by clientId(Cedula)
                Reporte reporte = DataSource().FirstOrDefault(singleReporte => singleReporte.Id == ReporteID);
                return reporte;
            }
            public Reporte Update(Reporte newReporte) //(U) PUT
            {
                //logic to Update a pedido
                var oldReporte = Delete(newReporte.Id);
                if (oldReporte != null)
                {
                    Insert(newReporte);
                    return newReporte;
                }
                return null;
            }
            public Reporte Delete(int ReporteID)     //(D) DELETE
            {
                //logic to Delete 
                List<Reporte> pedidosList = DataSource(); // Base de datos actual

                Reporte reporte = pedidosList.SingleOrDefault(singleReporte => singleReporte.Id == ReporteID);
                if (reporte == null)
                {
                    return null; //Si el student que se desea borrar no existia, se retorna un null
                }

                pedidosList.Remove(reporte);
                Serialize(pedidosList); //Almacenamos la ultima version de la base de datos

                return reporte; //Se retorna el student como convension para que se sepa que el mismo si existia en la base de datos

            }
            private List<Reporte> DataSource()
            {
                /* --------------------------------- SourceData Method -----------------------------------*/
                var jsonString = File.ReadAllText(jsonFilePath);

                //Console.WriteLine(jsonString);

                Reporte[] reporte = JsonConvert.DeserializeObject<Reporte[]>(jsonString);

                List<Reporte> reportesList = reporte.ToList();
                /* --------------------------------- SourceData Method -----------------------------------*/

                return reportesList;
            }
            private void Serialize(List<Reporte> pedidosList)
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
}
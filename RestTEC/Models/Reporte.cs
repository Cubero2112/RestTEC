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
        public List<Platillo> PlatillosMasVendidos { get; set; }
        public List<Platillo> PlatillosConMasGanancias { get; set; }
        public List<Platillo> PlatillosMejorFeedBack { get; set; }
        public List<Cliente> ClientesMasFieles { get; set; }


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
                //logic to return all 
                return DataSource();
            }

            /*
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
            */
            public Reporte GetReporte()
            {
                Reporte reporte = new Reporte()
                {
                    PlatillosMasVendidos = new List<Platillo>(),
                    PlatillosConMasGanancias = new List<Platillo>(),
                    PlatillosMejorFeedBack = new List<Platillo>(),
                    ClientesMasFieles = new List<Cliente>(),
                };

                PlatilloLogic platilloBL = new PlatilloLogic();
                List<Platillo> platillos = platilloBL.GetAll();

                //---------- Ordena los platillos del mas al menos vendido 
                platillos.Sort((x, y) => y.NumeroVentas.CompareTo(x.NumeroVentas));
                //---------- Ordena los platillos del mas al menos vendido 

                for (int i = 0; i < 10; i++)
                {
                    reporte.PlatillosMasVendidos.Add(platillos[i]);
                }


                //---------- Ordena los platillos del mas al que menos ganancias a generado 
                platillos.Sort((x, y) => (y.Precio * y.NumeroVentas).CompareTo(x.Precio * x.NumeroVentas));
                //---------- Ordena los platillos del mas al que menos ganancias a generado

                for (int i = 0; i < 10; i++)
                {
                    reporte.PlatillosConMasGanancias.Add(platillos[i]);
                }


                //---------- Ordena los platillos de que tiene mejor a peor feedback
                platillos.Sort((x, y) => (x.Feedback).CompareTo(y.Feedback));
                //---------- Ordena los platillos de que tiene mejor a peor feedback


                for (int i = 0; i < 10; i++)
                {

                    reporte.PlatillosMejorFeedBack.Add(platillos[i]);
                }

                reporte.ClientesMasFieles.Add(new Cliente() { Nombre = "Cliente Fiel" });

                return reporte;
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
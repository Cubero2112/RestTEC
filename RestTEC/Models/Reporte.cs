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
                platillos.Sort((x, y) => (y.Feedback).CompareTo(x.Feedback));
                //---------- Ordena los platillos de que tiene mejor a peor feedback


                for (int i = 0; i < 10; i++)
                {

                    reporte.PlatillosMejorFeedBack.Add(platillos[i]);
                }

                reporte.ClientesMasFieles.Add(new Cliente() { Nombre = "Cliente Fiel" });

                return reporte;
            }
            
        }

    }
}
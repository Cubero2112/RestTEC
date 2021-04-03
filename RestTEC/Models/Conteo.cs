using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestTEC.Models
{
    public class Conteo
    {
        public int Platillos { get; set; }
        public int Pedidos { get; set; }
        public int Usuarios { get; set; }
    }

    public class ConteoBL
    {

        private static string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "conteos.json"));
        private static Conteo DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            Conteo conteo = JsonConvert.DeserializeObject<Conteo>(jsonString);            
            /* --------------------------------- SourceData Method -----------------------------------*/

            return conteo;
        }
        public static int AumentarPlatillos()
        {
            Conteo conteo = DataSource();
            
            int nuevoNumeroPlatillos = ++conteo.Platillos;

            Serialize(conteo);

            return nuevoNumeroPlatillos;
        }
        public static int AumentarPedidos()
        {
            Conteo conteo = DataSource();

            int nuevoNumeroPedido = ++conteo.Pedidos;

            Serialize(conteo);
            return nuevoNumeroPedido;            
        }
        public static int AumentarUsuarios()
        {
            Conteo conteo = DataSource();

            int nuevoNumeroUsuarios = ++conteo.Usuarios;

            Serialize(conteo);
            return nuevoNumeroUsuarios;
        }                        
        private static void Serialize(Conteo conteo)
        {
            /* ------------------- Serialize Method -----------------------*/
            
            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, conteo);
            }
            /* ------------------- Serialize Method ----------------------- */
        }
    }

}
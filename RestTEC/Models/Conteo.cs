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

        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "conteos.json"));

        private Conteo DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            Conteo conteo = JsonConvert.DeserializeObject<Conteo>(jsonString);            
            /* --------------------------------- SourceData Method -----------------------------------*/

            return conteo;
        }
        public int AumentarPlatillos()
        {
            Conteo conteo = DataSource();
            
            int nuevoNumeroPlatillos = ++conteo.Platillos;

            Serialize(conteo);

            return nuevoNumeroPlatillos;
        }
        public int AumentarPedidos()
        {
            Conteo conteo = DataSource();

            int nuevoNumeroPedido = ++conteo.Pedidos;

            Serialize(conteo);
            return nuevoNumeroPedido;            
        }
        public int AumentarUsuarios()
        {
            Conteo conteo = DataSource();

            int nuevoNumeroUsuarios = ++conteo.Usuarios;

            Serialize(conteo);
            return nuevoNumeroUsuarios;
        }                        
        private void Serialize(Conteo conteo)
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestTEC.Models
{
    public class Menu
    {
        public List<Platillo> platillos { get; set; }
    }


    public class MenuLogic
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "menu.json"));
        private Menu DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            //Console.WriteLine(jsonString);

            Menu menu = JsonConvert.DeserializeObject<Menu>(jsonString);
            
            /* --------------------------------- SourceData Method -----------------------------------*/

            return menu;
        }
        public void Insert(int codigoPlatillo)
        {
            Menu menu = DataSource(); //Deserealizamos la base de datos en su ultima version
            
            /* ------------------- Post/insert Method -----------------------*/
            PlatilloLogic platilloLogic = new PlatilloLogic();
            Platillo platillo = platilloLogic.GetById(codigoPlatillo);

            if(menu.platillos == null)
            {
                menu.platillos = new List<Platillo>();
            }

            menu.platillos.Add(platillo);
           
            Serialize(menu); //Almacenamos la ultima version de la base de datos
            /* ------------------- Post/insert Method -----------------------*/
        }
        public Menu GetMenu()
        {            
            return DataSource();
        }
        public Platillo Delete(int codigoPlatillo)
        {
            //logic to Delete 
            Menu menu = DataSource(); // Base de datos actual

            Platillo platillo = menu.platillos.SingleOrDefault(singlePlatillo => singlePlatillo.Codigo == codigoPlatillo);
            if (platillo == null)
            {
                return null; 
            }

            menu.platillos.Remove(platillo);

            Serialize(menu); //Almacenamos la ultima version de la base de datos

            return platillo; //Se retorna el student como convension para que se sepa que el mismo si existia en la base de datos

        }
        private void Serialize(Menu menu)
        {
            /* ------------------- Serialize Method -----------------------*/
            //studentsList.ToArray();
            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, menu);
            }
            /* ------------------- Serialize Method -----------------------*/
        }

    }
}
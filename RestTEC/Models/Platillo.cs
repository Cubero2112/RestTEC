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
        public double Feedback { get; set; }
    }

    public class PlatilloLogic
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "platillos.json"));
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
        public List<Platillo> GetAll()
        {
            //(R) GET
            
            return DataSource();
        }
        public Platillo Insert(Platillo platillo)
        {
            //(C) POST                         
            List<Platillo> platilloList = DataSource(); // Base de datos actual deserealizada

            if( (0 < platillo.Precio) && (platillo.Descripcion.Length <= 100) && (0 < platillo.Calorias))
            {
                ConteoBL conteoBL = new ConteoBL();
                int actualNumeroPlatillos = conteoBL.AumentarPlatillos();

                platillo.Codigo = actualNumeroPlatillos;
                platilloList.Add(platillo);

                Serialize(platilloList); //Almacenamos la ultima version de la base de datos
                return platillo;
            }
            else
            {
                return null;
            }

            
        }
        public Platillo GetByCodigo(int Codigo)
        {
            //(R) GET
            Platillo platillo = DataSource().FirstOrDefault(singlePlatillo => singlePlatillo.Codigo == Codigo);
            if(platillo== null)
            {
                return null;
            }
            return platillo;
        }
        public Platillo Update(Platillo actualPlatillo)
        {
            //(U) PUT                        
            var oldPlatillo = Delete(actualPlatillo.Codigo);

            if (oldPlatillo != null)
            {
                actualPlatillo = Insert(actualPlatillo);
                if(actualPlatillo != null) //Indica que se hizo la insercion (El nuevo platillo cumplia con lo necesario)
                {

                    return oldPlatillo;
                }
                else //Indica que no se hizo la insercion (El platillo tenia valores no validos para poder la insercion)
                {
                    Insert(oldPlatillo);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public Platillo UpdateNumeroVentas(int Codigo)
        {            
            Platillo platillo = DataSource().FirstOrDefault(singlePlatillo => singlePlatillo.Codigo == Codigo);

            if(platillo == null)
            {
                return null;
            }
            else
            {
                platillo.NumeroVentas++;
                Update(platillo);
                return platillo;
            }            
        }
        public Platillo UpdateFeedBack(int Codigo, double Feedback)
        {
            Platillo platillo = DataSource().FirstOrDefault(singlePlatillo => singlePlatillo.Codigo == Codigo);
            if(platillo != null && 0 < Feedback)
            {
                platillo.Feedback = (platillo.Feedback + Feedback) / 2;
                Platillo oldPlatillo = Update(platillo);
                return oldPlatillo;
            }
            else
            {
                return null;
            }
        }
        public Platillo Delete(int Codigo)
        {
            //(D) DELETE
            
            List<Platillo> pedidosList = DataSource(); // Base de datos actual

            Platillo platillo = pedidosList.SingleOrDefault(singlePlatillo => singlePlatillo.Codigo == Codigo);
            if (platillo == null)
            {
                return null; 
            }
            ConteoBL conteoBL = new ConteoBL();
            int numeroPlatillos = conteoBL.DisminuirPlatillos();

            pedidosList.Remove(platillo);
            Serialize(pedidosList); //Almacenamos la ultima version de la base de datos

            return platillo; //Se retorna el platillo eliminado como convension para que se sepa que el mismo si existia en la base de datos

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
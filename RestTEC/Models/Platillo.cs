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
        public double TiempoPreparacion { get; set;}
        public double Feedback { get; set; }
    }

    public class PlatilloLogic
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "platillos.json"));
        private List<Platillo> DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

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
        public Platillo GetByCodigo(int Codigo)
        {
            //(R) GET
            Platillo platillo = DataSource().FirstOrDefault(singlePlatillo => singlePlatillo.Codigo == Codigo);
            if (platillo == null)
            {
                return null;
            }
            return platillo;
        }
        public Platillo Insert(Platillo platillo)
        {
            //(C) POST                         
            List<Platillo> platilloList = DataSource(); // Base de datos actual deserealizada

            string nombre = platillo.Nombre;
            string descripcion = platillo.Descripcion;
            double precio = platillo.Precio;
            double calorias = platillo.Calorias;
            
            if ((0 < precio) && (descripcion.Length <= 100) && (0 < calorias) && (nombre.Length <= 20))
            {
                int actualNumeroPlatillos = ConteoBL.AumentarPlatillos();

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
        public Platillo Update(Platillo platilloNuevaVersion)
        {
            //(U) PUT
            string nombre = platilloNuevaVersion.Nombre;
            string descripcion = platilloNuevaVersion.Descripcion;
            double precio = platilloNuevaVersion.Precio;
            double calorias = platilloNuevaVersion.Calorias;
            int codigo = platilloNuevaVersion.Codigo;

            if ((0 < precio) && (descripcion.Length <= 100) && (0 < calorias) && (nombre.Length <= 20))
            {
                if (GetByCodigo(codigo) != null) {
                    List<Platillo> platillosList = GetAll();

                    Platillo platilloARemplazar = platillosList.SingleOrDefault(singlePlatillo => singlePlatillo.Codigo == codigo);

                    int indexOfPlatilloARemplazar = platillosList.IndexOf(platilloARemplazar);

                    if(indexOfPlatilloARemplazar != -1)
                    {

                        platillosList[indexOfPlatilloARemplazar] = platilloNuevaVersion;
                        Serialize(platillosList);
                        return platilloARemplazar;
                    }
                    else // No se puede realizar el update pues no se encontro en la DB el platillo que se quiere actualizar.
                    {
                        return null;
                    }
                }
                else // No se pudo realizar el update pues en realidad no existe el item que se quiere actualizar en la base de datos
                {
                    return null;
                }
            }else // No se pudo realizar el update pues el nuevo platillo no cumple con los requisitos necesarios.
            {
                return null;
            }
        }
        public Platillo UpdateNumeroVentas(int Codigo)
        {
            Platillo platillo = DataSource().FirstOrDefault(singlePlatillo => singlePlatillo.Codigo == Codigo);

            if (platillo == null)
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
            if (platillo != null && 0 < Feedback)
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

            if (platillo != null)
            {             
                pedidosList.Remove(platillo);
                Serialize(pedidosList); //Almacenamos la ultima version de la base de datos

                return platillo; //Se retorna el platillo eliminado como convension para que se sepa que el mismo si existia en la base de datos
            }
            else
            {
                return null;
            }
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestTEC.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Dept { get; set; }
        public int Salary { get; set; }
    }


    public class EmployeeBL
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "employees.json"));
        public List<Employee> GetEmployees()
        {
            // In Realtime you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we are hardcoded the data

            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            //Console.WriteLine(jsonString);

            Employee[] employees = JsonConvert.DeserializeObject<Employee[]>(jsonString);

            List<Employee> employeesList = employees.ToList();
            /* --------------------------------- SourceData Method -----------------------------------*/

            return employeesList;
        }

        public void InsertEmployee(Employee newEmployee)
        {
            var employeesList = GetEmployees();
            employeesList.Add(newEmployee);
            Serialize(employeesList);
        }

        private void Serialize(List<Employee> employeesList)
        {

            /* ------------------- Serialize Method -----------------------*/
            //studentsList.ToArray();
            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, employeesList);
            }
            /* ------------------- Serialize Method -----------------------*/
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace RestTEC.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Ratings { get; set; }
        public Location Address { get; set; }
        
    }
    public class Location
    {
        public int Provincia { get; set; }
        public string Canton { get; set; }
    }
    public class StudentLogic
    {
        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "students.json"));
        private List<Student> DataSource()
        {
            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

            //Console.WriteLine(jsonString);

            Student[] students = JsonConvert.DeserializeObject<Student[]>(jsonString);


            List<Student> studentsList = students.ToList();
            /* --------------------------------- SourceData Method -----------------------------------*/

            return studentsList;
        }
        public IEnumerable<Student> GetAll()
        {
            //logic to return all employees
            return DataSource();
        }
        public Student GetById(int StudentID)
        {
            //logic to return an employee by employeeId
            Student student = DataSource().FirstOrDefault(singleStudent => singleStudent.Id == StudentID);
            return student;
        }
        public void Insert(Student student)
        {
            //logic to insert an student
            /* ------------------- Post/insert Method -----------------------*/
            List<Student> studentList = DataSource(); // Base de datos actual
            studentList.Add(student);
            Serialize(studentList); //Almacenamos la ultima version de la base de datos
            /* ------------------- Post/insert Method -----------------------*/
        }
        public void InsertRating(int StudentID, int Rating)
        {
            List<Student> studentList = DataSource(); // Base de datos actual desereliazada

            var student = studentList.SingleOrDefault(singleStudent => singleStudent.Id == StudentID);
            if (student != null)
            {
                if (student.Ratings == null)
                {
                    student.Ratings = new int[] { Rating };
                }
                else
                {
                    var ratings = student.Ratings.ToList();
                    ratings.Add(Rating);
                    student.Ratings = ratings.ToArray();
                }
                studentList.Remove(student);
                studentList.Add(student);
            }
            Serialize(studentList);
        }
        public Student Update(Student student)
        {
            //logic to Update an student
            var localStudent = Delete(student.Id);
            if (student != null)
            {
                Insert(student);
                return student;
            }
            return null;
        }
        public Student Delete(int StudentID)
        {
            //logic to Delete an student
            List<Student> studentList = DataSource(); // Base de datos actual

            var student = studentList.SingleOrDefault(singleStudent => singleStudent.Id == StudentID);
            if (student == null)
            {
                return null; //Si el student que se desea borrar no existia, se retorna un null
            }

            studentList.Remove(student);
            Serialize(studentList); //Almacenamos la ultima version de la base de datos

            return student; //Se retorna el student como convension para que se sepa que el mismo si existia en la base de datos

        }
        private void Serialize(List<Student> studentsList)
        {
            /* ------------------- Serialize Method -----------------------*/
            //studentsList.ToArray();
            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, studentsList);
            }
            /* ------------------- Serialize Method -----------------------*/
        }
    }
    public struct RatingStudent
    {
        public int StudentID { get; set; }
        public int Rating { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestTEC.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
    }

    public class UserBL
    {

        private string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "data", "users.json"));
        public List<User> GetUsers()
        {
            // In Realtime you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we are hardcoded the data

            /* --------------------------------- SourceData Method -----------------------------------*/
            var jsonString = File.ReadAllText(jsonFilePath);

           

            User[] users = JsonConvert.DeserializeObject<User[]>(jsonString);

            List<User> usersList = users.ToList();
            /* --------------------------------- SourceData Method -----------------------------------*/

            return usersList;
        }

        public void InsertUser(User newUser)
        {
            var usersList = GetUsers();
            usersList.Add(newUser);
            Serialize(usersList);
        }

        private void Serialize(List<User> usersList)
        {

            /* ------------------- Serialize Method -----------------------*/
            //studentsList.ToArray();
            File.WriteAllText(jsonFilePath, string.Empty);

            using (StreamWriter file = File.CreateText(jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, usersList);
            }
            /* ------------------- Serialize Method -----------------------*/
        }
    }


}
using Newtonsoft.Json;
using RestTEC.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public UserToken UserLogin(User userLogin)
        {            
            var userFound = UserValidate.GetUserDetails(userLogin.UserName, userLogin.Password);
            if(userFound != null)
            {
                string encodeString = $"{userLogin.UserName}:{userLogin.Password}";
                UserToken userToken = new UserToken()
                {
                    UserName = userFound.UserName,
                    Role = userFound.Roles,
                    Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(encodeString))
                };

                return userToken;
            }
            return null;
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
    public class UserToken
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
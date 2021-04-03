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
        public User GetByUserName(string userName)
        {
            //(Read) GET         
            User actualUser = GetUsers().FirstOrDefault(user => user.UserName.Equals(userName));
            if (actualUser == null)
            {
                return null;
            }
            return actualUser;
        }
        public User InsertUser(User newUser)
        {
            var userLists = GetUsers();

            bool existedUser = userLists.Any(user => user.UserName.Equals(newUser.UserName));
            string email = newUser.Email;

            if (existedUser || !(email.Contains('@'))) //No se puede registrar si envia un correo sin @ o si ya el userName esta siendo usado por otro usuario
            {
                return null;
            }

            ConteoBL conteoBL = new ConteoBL();
            int nuevoUser = conteoBL.AumentarUsuarios();

            userLists.Add(newUser);

            Serialize(userLists);

            return newUser;
        }
        public UserToken UserLogin(User userLogin)
        {
            var userFound = UserValidate.Login(userLogin.UserName, userLogin.Password);
            if (userFound) //Si el usuario ya se encuentra registrado en la base de datos se le daran sus credenciales (Token)
            {
                var userInDB = UserValidate.GetUserDetails(userLogin.UserName, userLogin.Password);

                string encodeString = $"{userLogin.UserName}:{userLogin.Password}";

                UserToken userToken = new UserToken()
                {
                    UserName = userInDB.UserName,
                    Role = userInDB.Roles,
                    Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(encodeString))
                };

                return userToken;
            }
            else
            {
                return null;
            }
        }
        public User Delete(string UserName)
        {
            //(D) DELETE
            List<User> userList = GetUsers(); // Base de datos actual

            User user = userList.SingleOrDefault(singleUser => singleUser.UserName.Equals(UserName));
            if (user == null)
            {
                return null;
            }

            ConteoBL conteoBL = new ConteoBL();
            //int numeroUser = conteoBL.DisminuirUsuarios();


            userList.Remove(user);
            Serialize(userList);
            return user;
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
using RestTEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestTEC.Controllers
{
    public class UserRegistrationController : ApiController
    {
        [HttpPost]
        [Route("registration")] //localhost:2342/UserRegistration/registration
        public HttpResponseMessage Registration([FromBody] ClientRegistration newClient)
        {
            var userBL = new UserBL();
            var clientBL = new ClienteBL();

            var user = new User() { 
                UserName = newClient.UserName,
                Password = newClient.Password,
                Email = newClient.Email,                
                Roles = "Client"
            };
            var client = new Cliente()
            {
                Cedula = newClient.Cedula,
                Nombre = newClient.Nombre,
                Apellido = newClient.Apellido,
                UserName = newClient.UserName,
                DiaNacimiento = newClient.DiaNacimiento,
                MesNacimiento = newClient.MesNacimiento,
                Provincia = newClient.Provincia,
                Canton = newClient.Canton,
                Distrito = newClient.Distrito,
                Telefono = newClient.Telefono
                
            };     
            
            var userCreated = userBL.InsertUser(user);
            if(userCreated == null) //Existe un usuario con ese UserName en la base de datos por tanto el nuevo user no será creado
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            else
            {
                var clientCreated = clientBL.Insert(client);
                if(clientCreated == null) // Si no es posible crear al nuevo cliente. (Nota: Aun no se cual podria ser una posible restriccion en este punto)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
    }
}

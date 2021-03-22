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
        public HttpResponseMessage Registration([FromBody] User newUser)
        {
            var userBL = new UserBL();
            userBL.InsertUser(newUser);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

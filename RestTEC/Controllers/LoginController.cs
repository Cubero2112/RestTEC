using RestTEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RestTEC.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Login")]
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        public HttpResponseMessage Login([FromBody] User loginUser)
        {
            UserBL userBL = new UserBL();
            var userToken = userBL.UserLogin(loginUser);
            if(userToken == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userToken);
        }
    }
}

using RestTEC.Authentication;
using RestTEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace RestTEC.Controllers
{   //localhost:97234/Admi/getPlatillos

    [BasicAuthentication]
    [MyAuthorize(Roles = "Admin")]
    public class AdmiController : ApiController
    {
        [HttpGet]
        [Route("getPlatillos")]
        public HttpResponseMessage GetPlatillos()
        {
            PlatilloLogic platilloBL = new PlatilloLogic();
            IEnumerable<Platillo> platillos = platilloBL.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, platillos);
        }


        [HttpPut]
        [Route("actualizarPlatillo")]
        public HttpResponseMessage ActualizarPlatillo([FromBody] Platillo platillo)
        {

            PlatilloLogic platilloBL = new PlatilloLogic();
            platilloBL.Update(platillo);


            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}

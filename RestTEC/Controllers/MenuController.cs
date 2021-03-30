using RestTEC.Authentication;
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
    [BasicAuthentication]
    [MyAuthorize(Roles = "Admin,Client,Chef")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class MenuController : ApiController
    {
        [HttpGet]
        [Route("menu/get")]
        public HttpResponseMessage GetMenu()
        {
            MenuLogic menuLogic = new MenuLogic();
            Menu menu = menuLogic.GetMenu();
            return Request.CreateResponse(HttpStatusCode.OK, menu);

        }

        [HttpPost]
        [Route("menu/savePlatillo")]
        public HttpResponseMessage SavePlatillo([FromBody] Platillo platillo)
        {
            MenuLogic menuLogic = new MenuLogic();
            menuLogic.Insert(platillo.Codigo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpDelete]
        [Route("menu/deletePlatillo/{codigoPlatillo}")]
        public HttpResponseMessage DeletePlatillo(int codigoPlatillo)
        {
            MenuLogic menuLogic = new MenuLogic();
            Platillo platillo = menuLogic.Delete(codigoPlatillo);

            return Request.CreateResponse(HttpStatusCode.OK, platillo);
        }
    }
}

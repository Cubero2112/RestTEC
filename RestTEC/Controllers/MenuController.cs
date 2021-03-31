using RestTEC.Authentication;
using RestTEC.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RestTEC.Controllers
{

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class MenuController : ApiController
    {
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin,Client")]
        [HttpGet]
        [Route("menu/get")]
        public HttpResponseMessage GetMenu()
        {
            MenuLogic menuLogic = new MenuLogic();
            Menu menu = menuLogic.GetMenu();
            return Request.CreateResponse(HttpStatusCode.OK, menu);

        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin")]
        [HttpPost]
        [Route("menu/savePlatillo")]
        public HttpResponseMessage SavePlatillo([FromBody] Platillo platillo)
        {
            MenuLogic menuLogic = new MenuLogic();
            menuLogic.Insert(platillo.Codigo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin")]
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

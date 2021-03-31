using RestTEC.Authentication;
using RestTEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;
using static RestTEC.Models.Reporte;

namespace RestTEC.Controllers
{   //localhost:97234/Admi/getPlatillos

    [BasicAuthentication]
    [MyAuthorize(Roles = "Admin")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AdmiController : ApiController
    {
        [HttpGet]
        [Route("admi/getPlatillos")]
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

        [HttpPut]
        [Route("eliminarPlatillo")]
        public HttpResponseMessage EliminarPlatillo([FromBody] Platillo platillo)
        {

            PlatilloLogic platilloBL = new PlatilloLogic();
            platilloBL.Delete(platillo.Codigo);


            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("insertPlatillo")]
        public HttpResponseMessage InsertPlatillo([FromBody] Platillo platillo)
        {
            PlatilloLogic platilloBL = new PlatilloLogic();
            platilloBL.Insert(platillo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpGet]
        [Route("getReporte")]
        public HttpResponseMessage GetReporte()
        {

            ReporteLogic reporteBL = new ReporteLogic();
            Reporte reporte = reporteBL.GetReporte();

            
            return Request.CreateResponse(HttpStatusCode.OK, reporte);
        }

    }
}

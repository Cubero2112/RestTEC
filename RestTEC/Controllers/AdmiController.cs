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

        //Bajo el URL: https://localhost:44381/admi/getPlatillos cualquiera registrado como admi puede solicitar todos los platillos disponibles en RestTEC
        public HttpResponseMessage GetPlatillos() 
        {
            PlatilloLogic platilloBL = new PlatilloLogic();
            IEnumerable<Platillo> platillos = platilloBL.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, platillos);
        }


        [HttpPut]
        [Route("actualizarPlatillo")]

        //Bajo el URL: https://localhost:44381/actualizarPlatillo cualquiera registrado como admi puede actualizar un platillo, esto, pasando el codigo 
        // del platillo que se quiere actualizar mas toda la informacion necesario para hacer la actualizacion
        public HttpResponseMessage ActualizarPlatillo([FromBody] Platillo platillo)
        {

            PlatilloLogic platilloBL = new PlatilloLogic();
            platilloBL.Update(platillo);


            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("eliminarPlatillo")]

        //Bajo el URL: https://localhost:44381/actualizarPlatillo cualquiera registrado como admi puede eliminar un platillo, esto, pasando el codigo
        //del platillo que se quiere eliminar
        public HttpResponseMessage EliminarPlatillo([FromBody] Platillo platillo)
        {

            PlatilloLogic platilloBL = new PlatilloLogic();
            platilloBL.Delete(platillo.Codigo);


            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("insertPlatillo")]

        //Bajo el URL: https://localhost:44381/insertPlatillo cualquiera registrado como admi puede insertar un platillo, esto, pasando el platillo 
        //en formato JSON que quiere insertar
        public HttpResponseMessage InsertPlatillo([FromBody] Platillo platillo)
        {
            PlatilloLogic platilloBL = new PlatilloLogic();
            platilloBL.Insert(platillo);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpGet]
        [Route("getReporte")]

        //Bajo el URL: https://localhost:44381/getReporte cualquiera registrado como admi puede obtener un reporte de: los platillos con mejor feedback, 
        //los mas vendidos, los clientes mas fieles y los platillos que mas ganancias han dejado de entre los vendidos
        public HttpResponseMessage GetReporte()
        {

            ReporteLogic reporteBL = new ReporteLogic();
            Reporte reporte = reporteBL.GetReporte();

            
            return Request.CreateResponse(HttpStatusCode.OK, reporte);
        }

    }
}

using RestTEC.Authentication;
using RestTEC.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace RestTEC.Controllers
{
    [BasicAuthentication]
    [MyAuthorize(Roles = "Client")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ClientController : ApiController
    {

        [HttpGet]
        [Route("client/getPlatillos")]
        public HttpResponseMessage GetPlatillos()
        {
            PlatilloLogic platilloBL = new PlatilloLogic();
            IEnumerable<Platillo> platillos = platilloBL.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, platillos);
        }

        [HttpPost]
        [Route("client/pedido")]
        public HttpResponseMessage Pedido([FromBody]Pedido pedido)
        {
            PedidoLogic pedidoLogic = new PedidoLogic();
            pedido = pedidoLogic.Insert(pedido);
            if(pedido == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            else
            {
                string Username = BasicAuthenticationAttribute.UserNameActual;   
                
                ClienteBL clientBL = new ClienteBL();
                Cliente client = clientBL.InsertPedidoAClienteByOrderNumber(pedido.Orden, Username);

                return Request.CreateResponse(HttpStatusCode.OK, pedido);

            }
        }


    }
}

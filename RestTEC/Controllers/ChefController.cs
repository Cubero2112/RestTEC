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
    [MyAuthorize(Roles = "Chef")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ChefController : ApiController
    {
        [HttpGet]
        [Route("chef/getPedidos")]
        public HttpResponseMessage GetPedidosNoAsignados()
        {
            PedidoLogic pedidoLogic = new PedidoLogic();
            List<Pedido> pedidosNoAsignados = pedidoLogic.GetPedidosNoAsignados();

            return Request.CreateResponse(HttpStatusCode.OK, pedidosNoAsignados);
        }

        [HttpGet]
        [Route("chef/getMisPedidos")]
        public HttpResponseMessage GetMisPedidos()
        {
            PedidoLogic pedidoLogic = new PedidoLogic();

            string Username = BasicAuthenticationAttribute.UserNameActual;

            List<Pedido> misPedidos = pedidoLogic.GetChefPedidos(Username);

            return Request.CreateResponse(HttpStatusCode.OK, misPedidos);
        }


        [HttpPost]
        [Route("chef/InitPedido")]
        public HttpResponseMessage InitPedido([FromBody] Pedido pedidoWeb)
        {
            PedidoLogic pedidoLogic = new PedidoLogic();

            string Username = BasicAuthenticationAttribute.UserNameActual;

            Pedido pedido = pedidoLogic.InitPedido(pedidoWeb.Orden, Username);

            return Request.CreateResponse(HttpStatusCode.OK, pedido);
        }

        [HttpPost]
        [Route("chef/FinishPedido")]
        public HttpResponseMessage FinishPedido([FromBody] Pedido pedidoWeb)
        {
            PedidoLogic pedidoLogic = new PedidoLogic();
            ClienteBL clienteBL = new ClienteBL();

            string Username = BasicAuthenticationAttribute.UserNameActual;

            Pedido pedido = pedidoLogic.FinishPedido(pedidoWeb.Orden, Username);
            if (pedido == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            int numeroOrdenLista = clienteBL.RemoveActualPedido(pedidoWeb.Orden);

            return Request.CreateResponse(HttpStatusCode.OK, pedido);
        }

    }
}
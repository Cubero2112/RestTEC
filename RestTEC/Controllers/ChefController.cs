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

        //Bajo el URL: https://localhost:44381/chef/getPedidos cualquiera registrado como chef puede obtener todos los pedidos que aun no han sido tomados por ningun chef
        public HttpResponseMessage GetPedidosNoAsignados()
        {
            PedidoLogic pedidoLogic = new PedidoLogic();
            List<Pedido> pedidosNoAsignados = pedidoLogic.GetPedidosNoAsignados();

            return Request.CreateResponse(HttpStatusCode.OK, pedidosNoAsignados);
        }

        [HttpGet]
        [Route("chef/getMisPedidos")]

        //Bajo el URL: https://localhost:44381/chef/getMisPedidos cualquiera registrado como chef puede obtener todos los pedidos que este mismo se ha asignado
        public HttpResponseMessage GetMisPedidos()
        {
            PedidoLogic pedidoLogic = new PedidoLogic();
            
            List<Pedido> misPedidos = pedidoLogic.GetChefPedidos();

            return Request.CreateResponse(HttpStatusCode.OK, misPedidos);
        }

        [HttpPost]
        [Route("chef/InitPedido")]

        //Bajo el URL: https://localhost:44381/chef/InitPedido cualquiera registrado como chef puede asignarse un pedido mediante este URL
        public HttpResponseMessage InitPedido([FromBody]Pedido pedidoWeb)
        {
            PedidoLogic pedidoLogic = new PedidoLogic();            

            Pedido pedido = pedidoLogic.InitPedido(pedidoWeb.Orden);

            return Request.CreateResponse(HttpStatusCode.OK, pedido);
        }

        [HttpPost]
        [Route("chef/FinishPedido")]

        //Bajo el URL: https://localhost:44381/chef/FinishPedido cualquiera registrado como chef puede finalizar un pedido que este mismo haya iniciado mediante este URL 
        public HttpResponseMessage FinishPedido([FromBody] Pedido pedidoWeb)
        {
            PedidoLogic pedidoLogic = new PedidoLogic();
            ClienteBL clienteBL = new ClienteBL();


            Pedido pedido = pedidoLogic.FinishPedido(pedidoWeb.Orden);
            if(pedido == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            int numeroOrdenLista = clienteBL.RemoveActualPedido(pedidoWeb.Orden);

            return Request.CreateResponse(HttpStatusCode.OK, pedido);
        }

    }
}

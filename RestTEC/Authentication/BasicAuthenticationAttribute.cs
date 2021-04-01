using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RestTEC.Authentication
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private const string Realm = "My Realm";
        public static string UserNameActual;
        public static string PassWordActual;
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null) // Si no se envia en el encabezado del request las credenciales para hacer el Login
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                if (actionContext.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
                }
            }
            else // Si se envia en el encabezado del request las credenciales para hacer el Login
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                UserNameActual = usernamePasswordArray[0];
                PassWordActual = usernamePasswordArray[1];

                if (UserValidate.Login(username, password)) //Si existe un user tal que ya se haya registrato (Que se encuentre en la base de datos).
                {
                    var UserDetails = UserValidate.GetUserDetails(username, password);
                    var identity = new GenericIdentity(username);
                    identity.AddClaim(new Claim("Email", UserDetails.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Name, UserDetails.UserName));
                    //identity.AddClaim(new Claim("ID", Convert.ToString(UserDetails.ID)));
                    IPrincipal principal = new GenericPrincipal(identity, UserDetails.Roles.Split(','));
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                }
                else // Si no existe un user que ya se haya registrado quiere decir que, quien hace el request no esta autorizado (No se encuentra en la base de datos)
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}
using RestTEC.Authentication;
using RestTEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace RestTEC.Controllers
{
    public class EmployeeController : ApiController
    {        
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin")]
        [Route("AllMaleEmployees")]
        public HttpResponseMessage GetAllMaleEmployees()
        {
            var identity = (ClaimsIdentity)User.Identity;

            //Getting the ID value
            var ID = identity.Claims
                       .FirstOrDefault(c => c.Type == "ID").Value;

            //Getting the Email value
            var Email = identity.Claims
                      .FirstOrDefault(c => c.Type == "Email").Value;

            //Getting the Username value
            var username = identity.Name;

            //Getting the Roles only if you set the roles in the claims
            //var Roles = identity.Claims
            //            .Where(c => c.Type == ClaimTypes.Role)
            //            .Select(c => c.Value).ToArray();

            var EmpList = new EmployeeBL().GetEmployees().Where(e => e.Gender.ToLower() == "male").ToList();

            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Client")]
        [Route("AllFemaleEmployees")]
        [HttpGet]
        public HttpResponseMessage GetAllFemaleEmployees()
        {
            var EmpList = new EmployeeBL().GetEmployees().Where(e => e.Gender.ToLower() == "female").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin,Client,Chef")]
        [Route("AllEmployees")]
        public HttpResponseMessage GetAllEmployees()
        {
            var EmpList = new EmployeeBL().GetEmployees();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }

        [BasicAuthentication]
        [MyAuthorize(Roles = "Chef")]
        [Route("InsertEmployee")]
        [HttpPost]
        public HttpResponseMessage InsertEmployee([FromBody] Employee employee)
        {

            var employeeBL = new EmployeeBL();
            employeeBL.InsertEmployee(employee);

            return Request.CreateResponse(HttpStatusCode.OK, employeeBL.GetEmployees());
        }
    }
}

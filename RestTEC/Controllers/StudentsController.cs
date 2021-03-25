using RestTEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestTEC.Controllers
{
    public class StudentsController : ApiController
    {

        [HttpGet]
        [Route("All")]
        public HttpResponseMessage GetAll() // Retorna todos los estudiantes
        {

            StudentLogic studentBL = new StudentLogic();
            /*
            if (studentsdata.DataSource().Count == 0)
            {
                return NotFound("No list Found.");
            }
            */
            return Request.CreateResponse(HttpStatusCode.OK, studentBL.GetAll());            
        }

        [HttpGet]
        public HttpResponseMessage GetByID(int StudentID) // Retorna estudiante con un id en particular
        {
            StudentLogic studentBL = new StudentLogic();
            Student student = studentBL.GetById(StudentID);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Student Not Found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage Save([FromBody] Student Student)
        {
            StudentLogic studentBL = new StudentLogic();
            studentBL.Insert(Student);
            /*
                if (studentsdata.DataSource().Count == 0)
                    {
                    return NotFound("No list Found.");
                    }
            */

            return Request.CreateResponse(HttpStatusCode.OK, studentBL.GetAll());
        }

        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage Update([FromBody] Student Student)
        {
            StudentLogic studentBL = new StudentLogic();
            var student = studentBL.Update(Student);

            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Student Not Found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, studentBL.GetAll()); 
        }

        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage Delete(int StudentID)
        {
            StudentLogic studentBL = new StudentLogic();
            var student = studentBL.Delete(StudentID);

            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Student Not Found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, studentBL.GetAll());
        }

        [HttpPut]
        [Route("Rating")]
        public HttpResponseMessage InsertRanting([FromBody] RatingStudent ratingStudent)
        {
            StudentLogic studentBL = new StudentLogic();
            studentBL.InsertRating(ratingStudent.StudentID, ratingStudent.Rating);
            return Request.CreateResponse(HttpStatusCode.OK, studentBL.GetAll());
        }


 

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using attendanceWeb.Models;

namespace attendanceWeb.Controllers
{
    public class loginController : ApiController
    {
        biometricsEntities db = new biometricsEntities();
        [ResponseType(typeof(lecturer))]
        public IHttpActionResult Post(login lecturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = db.lecturers.FirstOrDefault(e => e.staffNo == lecturer.user && e.password == lecturer.password);
            return Ok(result);

            //return CreatedAtRoute("DefaultApi", new { id = lecturer.id }, lecturer);
        }
    }
}

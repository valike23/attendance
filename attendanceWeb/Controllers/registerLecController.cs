using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using attendanceWeb;
using attendanceWeb.Models;

namespace attendanceWeb.Controllers
{
    public class registerLecController : ApiController
    {
        private biometricsEntities db = new biometricsEntities();
        public IHttpActionResult Postlecturer(addLecturer  AddLecturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
              
                var Course = db.courses.First(e => e.id == AddLecturer.CourseId);
                db.spUpdateLecturerCourse(AddLecturer.Lecturer.id, AddLecturer.CourseId);
               // Course.lecturers.;
                
                db.SaveChanges();
                return Ok(Course);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

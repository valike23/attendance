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
    public class lecturersController : ApiController
    {
        private biometricsEntities db = new biometricsEntities();

        // GET: api/lecturers
        public IQueryable<lecturer> Getlecturers()
        {
            return db.lecturers;
        }

        // GET: api/lecturers/5
        [ResponseType(typeof(lecturer))]
        public IHttpActionResult Getlecturer(int id)
        {
            lecturer lecturer = db.lecturers.Find(id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return Ok(lecturer);
        }

   
       

        // POST: api/lecturers
        [ResponseType(typeof(lecturer))]
        public IHttpActionResult Postlecturer(lecturer lecturer)
        {
            //done
            lecturer.password = "pass";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.lecturers.Add(lecturer);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = lecturer.id }, lecturer);

        }

        // DELETE: api/lecturers/5
        [ResponseType(typeof(lecturer))]
        public IHttpActionResult Deletelecturer(int id)
        {
            lecturer lecturer = db.lecturers.Find(id);
            if (lecturer == null)
            {
                return NotFound();
            }

            db.lecturers.Remove(lecturer);
            db.SaveChanges();

            return Ok(lecturer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool lecturerExists(int id)
        {
            return db.lecturers.Count(e => e.id == id) > 0;
        }
    }
}
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

namespace attendanceWeb.Controllers
{
    public class departmentsController : ApiController
    {
        private biometricsEntities db = new biometricsEntities();

        // GET: api/departments
        public IQueryable<department> Getdepartments()
        {
            return db.departments;
        }

        // GET: api/departments/5
        [ResponseType(typeof(department))]
        public IHttpActionResult Getdepartment(int id)
        {
            department department = db.departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/departments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdepartment(int id, department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.id)
            {
                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!departmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/departments
        [ResponseType(typeof(department))]
        public IHttpActionResult Postdepartment(department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.id }, department);
        }

        // DELETE: api/departments/5
        [ResponseType(typeof(department))]
        public IHttpActionResult Deletedepartment(int id)
        {
            department department = db.departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.departments.Remove(department);
            db.SaveChanges();

            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool departmentExists(int id)
        {
            return db.departments.Count(e => e.id == id) > 0;
        }
    }
}
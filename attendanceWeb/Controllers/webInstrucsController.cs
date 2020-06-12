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
    public class webInstrucsController : ApiController
    {
        private biometricsEntities db = new biometricsEntities();

        // GET: api/webInstrucs
        public IHttpActionResult GetwebInstrucs()
        {
            webInstruc web = new webInstruc();
            var webs = db.webInstrucs.Where(e => e.done == 0);
            foreach (var w in webs)
            {
                web = w;
            }
            web.done = 1;
            db.Entry(web).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {

            }
            finally
            {
              
            }
            return Ok(web);
           
        }

        // GET: api/webInstrucs/5
        [ResponseType(typeof(webInstruc))]
        public IHttpActionResult GetwebInstruc(int id)
        {
            webInstruc webInstruc = db.webInstrucs.Find(id);
            if (webInstruc == null)
            {
                return NotFound();
            }

            return Ok(webInstruc);
        }

        // PUT: api/webInstrucs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutwebInstruc(int id, webInstruc webInstruc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webInstruc.Id)
            {
                return BadRequest();
            }

            db.Entry(webInstruc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!webInstrucExists(id))
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

        // POST: api/webInstrucs
        [ResponseType(typeof(webInstruc))]
        public IHttpActionResult PostwebInstruc(webInstruc webInstruc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.webInstrucs.Add(webInstruc);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = webInstruc.Id }, webInstruc);
        }

        // DELETE: api/webInstrucs/5
        [ResponseType(typeof(webInstruc))]
        public IHttpActionResult DeletewebInstruc(int id)
        {
            webInstruc webInstruc = db.webInstrucs.Find(id);
            if (webInstruc == null)
            {
                return NotFound();
            }

            db.webInstrucs.Remove(webInstruc);
            db.SaveChanges();

            return Ok(webInstruc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool webInstrucExists(int id)
        {
            return db.webInstrucs.Count(e => e.Id == id) > 0;
        }
    }
}
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
    public class verficationsController : ApiController
    {
        private biometricsEntities db = new biometricsEntities();

        // GET: api/verfications
        public IHttpActionResult Getverfications()
        {
            verfication Verify = new verfication();
            var Verifys = db.verfications.Where(e => e.done == 0);
            foreach (var v in Verifys)
            {
                Verify = v;
            }
            Verify.done = 1;
            db.Entry(Verify).State = EntityState.Modified;
           
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            finally
            {

            }
            return Ok(Verify);
          //  return db.verfications;
        }

        // GET: api/verfications/5
        [ResponseType(typeof(verfication))]
        public IHttpActionResult Getverfication(int id)
        {
            verfication verfication = db.verfications.Find(id);
            if (verfication == null)
            {
                return NotFound();
            }

            return Ok(verfication);
        }

        // PUT: api/verfications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putverfication(int id, verfication verfication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != verfication.Id)
            {
                return BadRequest();
            }

            db.Entry(verfication).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!verficationExists(id))
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

        // POST: api/verfications
        [ResponseType(typeof(verfication))]
        public IHttpActionResult Postverfication(verfication verfication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.verfications.Add(verfication);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = verfication.Id }, verfication);
        }

        // DELETE: api/verfications/5
        [ResponseType(typeof(verfication))]
        public IHttpActionResult Deleteverfication(int id)
        {
            verfication verfication = db.verfications.Find(id);
            if (verfication == null)
            {
                return NotFound();
            }

            db.verfications.Remove(verfication);
            db.SaveChanges();

            return Ok(verfication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool verficationExists(int id)
        {
            return db.verfications.Count(e => e.Id == id) > 0;
        }
    }
}
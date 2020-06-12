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
    public class myclassesController : ApiController
    {
        private biometricsEntities db = new biometricsEntities();

        // GET: api/myclasses
        public IQueryable<myclass> Getmyclasses()
        {
            return db.myclasses;
        }

        // GET: api/myclasses/5
        [ResponseType(typeof(myclass))]
        public IHttpActionResult Getmyclass(int id)
        {
            //used
            myclass myclass = db.myclasses.Find(id);
            myclass.live = true;

            db.Entry(myclass).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(myclass);
        }

    

        // POST: api/myclasses
        [ResponseType(typeof(myclass))]
        public IHttpActionResult Postmyclass([FromUriAttribute] int id)
        {
            //done
           
            try
            {
                myclass myclass = db.myclasses.Find(id);
                myclass.cdone = true;
                myclass.live = false;

                db.Entry(myclass).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(myclass);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


           
        }

        // DELETE: api/myclasses/5
        [ResponseType(typeof(myclass))]
        public IHttpActionResult Deletemyclass(int id)
        {
            myclass myclass = db.myclasses.Find(id);
            if (myclass == null)
            {
                return NotFound();
            }

            db.myclasses.Remove(myclass);
            db.SaveChanges();

            return Ok(myclass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool myclassExists(int id)
        {
            return db.myclasses.Count(e => e.Id == id) > 0;
        }
    }
}
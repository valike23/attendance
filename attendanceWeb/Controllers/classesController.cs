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
    public class classesController : ApiController
    {
        private biometricsEntities db = new biometricsEntities();
        // GET: api/classes
        public IHttpActionResult Get()
        {
        var classes  =  db.myclasses;
          //  = ( from a in db.myclasses join b in db.courses on a.course equals b.id select new { a.date, a.Id, a.period, b.name, a.done, a.live } ).ToList();
            return Ok(classes);
        }

        // GET: api/classes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/classes
        public IHttpActionResult Post(myclass MyClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.myclasses.Add(MyClass);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = MyClass.Id }, MyClass);
        }

        // PUT: api/classes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/classes/5
        public void Delete(int id)
        {
        }
    }
}

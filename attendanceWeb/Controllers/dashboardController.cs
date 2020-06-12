using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using attendanceWeb.Models;

namespace attendanceWeb.Controllers
{
    public class dashboardController : ApiController
    {
        biometricsEntities db = new biometricsEntities();
        // GET: api/dashboard
        public IHttpActionResult Get()
        {
            dashboard Dashboard = new dashboard();
            Dashboard.students = db.students.Count();
            Dashboard.lecturers = db.lecturers.Count();
            Dashboard.courses = db.courses.Count();
            Dashboard.attendances = db.attendances.Count();
            return Ok(Dashboard);
        }

        // GET: api/dashboard/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/dashboard
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/dashboard/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/dashboard/5
        public void Delete(int id)
        {
        }
    }
}

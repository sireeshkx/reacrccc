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
using Newtonsoft.Json;
using System.Web.Http.Cors;
using System.Web;

namespace reactcrud.Controllers
{

    public class employees1Controller : ApiController
    {
        private kstestEntities db = new kstestEntities();

        // GET: api/Employees1
        [HttpGet]
        [Route("api/employees1/getemployee")]
        public  IHttpActionResult GetEmployees()
        {
            string clientAddress = HttpContext.Current.Request.UserHostAddress;

            var x = db.Employees.ToList();
            var j = JsonConvert.SerializeObject(x);
          var results = JsonConvert.DeserializeObject(j);

            return Json(x);

        }

        // GET: api/Employees1/5

        [HttpGet]
        [Route("api/employees1/getemployid")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees1/5
        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/employees1/EditEmployee")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees1
        [HttpPost]
        [Route("api/employees1/PostEmployee")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }

        // DELETE: api/Employees1/5
        [ResponseType(typeof(Employee))]
        [HttpGet]
        [Route("api/employees1/DeleteEmployee")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}


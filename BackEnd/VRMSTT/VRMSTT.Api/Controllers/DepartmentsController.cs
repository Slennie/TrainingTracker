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
using VRMSTT.Api.Infrastructure;
using VRMSTT.Api.Models;

namespace VRMSTT.Api.Controllers
{
    public class DepartmentsController : ApiController
    {
        private VRMSTTDataContext db = new VRMSTTDataContext();

        // GET: api/Departments
        public IHttpActionResult GetDepartments()
        {
            var resultSet = db.Departments.Select(d => new
            {
                d.DepartmentId,
                d.DepartmentName,
                d.Address,
                d.City,
                d.State,
                d.Zip,
                d.CreatedDate
            });
            return Ok(resultSet);
        }

        // GET: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult GetDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            var resultSet = new
            {
                department.DepartmentId,
                department.DepartmentName,
                department.City,
                department.State,
                department.Address,
                department.Zip,
                department.CreatedDate,
                Users = department.Users.Select(u => new
                {
                    u.Id,
                    u.PrimaryJobTitle,
                    u.FirstName,
                    u.LastName,
                    u.Unit,
                    u.Rank

                })
            };
            return Ok(resultSet);
        }

        // PUT: api/Departments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartment(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.DepartmentId)
            {
                return BadRequest();
            }
            var dbDepartment = db.Departments.Find(id);

            dbDepartment.DepartmentName = department.DepartmentName;
            dbDepartment.City = department.City;
            dbDepartment.State = department.State;
            dbDepartment.Address = department.Address;
            dbDepartment.Zip = department.Zip;


            db.Entry(dbDepartment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        [ResponseType(typeof(Department))]
        public IHttpActionResult PostDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
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

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentId == id) > 0;
        }
    }
}
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
    public class CoursesController : ApiController
    {
        private VRMSTTDataContext db = new VRMSTTDataContext();

        // GET: api/Courses
        public IHttpActionResult GetCourses()
        {
            var resultSet = db.Courses.Select(c => new
            {
                c.CourseId,
                c.UserId,
                c.Instructor,
                c.CourseTitle,
                c.CreatedDate,
                c.StartDate,
                c.EndDate,
                c.CertificateExpiration,
                c.location,
                c.PPERequired,
                c.MaterialsNeeded,
                c.SpecialInstructions,
                c.AvailableOnline,
                c.CertificateAwarded,
                c.EMSCE,
                c.Cost,
                c.PaymentOption,
                c.HostingOrganization,
                c.ReimbursingAgency,
                c.LabOrLecture
            }); 
            return Ok(resultSet);
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult GetCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            var resultSet =  new
            {
                course.CourseId,
                course.UserId,
                course.Instructor,
                course.CourseTitle,
                course.CreatedDate,
                course.StartDate,
                course.EndDate,
                course.CertificateExpiration,
                course.location,
                course.PPERequired,
                course.MaterialsNeeded,
                course.SpecialInstructions,
                course.AvailableOnline,
                course.CertificateAwarded,
                course.EMSCE,
                course.Cost,
                course.PaymentOption,
                course.HostingOrganization,
                course.ReimbursingAgency,
                course.LabOrLecture
            };
            return Ok(resultSet);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.CourseId)
            {
                return BadRequest();
            }

            var dbCourse = db.Courses.Find(id);

            dbCourse.Instructor = course.Instructor;
            dbCourse.CourseTitle = course.CourseTitle;
            dbCourse.CreatedDate = course.CreatedDate;
            dbCourse.StartDate = course.StartDate;
            dbCourse.EndDate = course.EndDate;
            dbCourse.CertificateExpiration = course.CertificateExpiration;
            dbCourse.location = course.location;
            dbCourse.PPERequired = course.PPERequired;
            dbCourse.MaterialsNeeded = course.MaterialsNeeded;
            dbCourse.SpecialInstructions = course.SpecialInstructions;
            dbCourse.AvailableOnline = course.AvailableOnline;
            dbCourse.CertificateAwarded = course.CertificateAwarded;
            dbCourse.EMSCE = course.EMSCE;
            dbCourse.Cost = course.Cost;
            dbCourse.PaymentOption = course.PaymentOption;
            dbCourse.HostingOrganization = course.HostingOrganization;
            dbCourse.ReimbursingAgency = course.ReimbursingAgency;
            dbCourse.LabOrLecture = course.LabOrLecture;
           


            db.Entry(dbCourse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        [ResponseType(typeof(Course))]
        public IHttpActionResult PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courses.Add(course);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.CourseId == id) > 0;
        }
    }
}
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
    public class PrimaryJobTitlesController : ApiController
    {
        private VRMSTTDataContext db = new VRMSTTDataContext();

        // GET: api/PrimaryJobTitles
        public IHttpActionResult GetPrimaryJobTitles()
        {
            var resultSet = db.PrimaryJobTitles.Select(p => new
            {
                p.PrimaryJobTitleId,
                p.UserId,
                p.Title

            });

            return Ok(resultSet);
        }

        // GET: api/PrimaryJobTitles/5
        [ResponseType(typeof(PrimaryJobTitle))]
        public IHttpActionResult GetPrimaryJobTitle(int id)
        {
            PrimaryJobTitle primaryJobTitle = db.PrimaryJobTitles.Find(id);
            if (primaryJobTitle == null)
            {
                return NotFound();
            }
            var resultSet = db.PrimaryJobTitles.Select(p => new
            {
                p.PrimaryJobTitleId,
                p.UserId,
                p.Title

            });

            return Ok(resultSet);
        }

        // PUT: api/PrimaryJobTitles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrimaryJobTitle(int id, PrimaryJobTitle primaryJobTitle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != primaryJobTitle.PrimaryJobTitleId)
            {
                return BadRequest();
            }

            var dbPrimaryJobTitle = db.PrimaryJobTitles.Find(id);

            dbPrimaryJobTitle.Title = primaryJobTitle.Title;



            db.Entry(dbPrimaryJobTitle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrimaryJobTitleExists(id))
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

        // POST: api/PrimaryJobTitles
        [ResponseType(typeof(PrimaryJobTitle))]
        public IHttpActionResult PostPrimaryJobTitle(PrimaryJobTitle primaryJobTitle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PrimaryJobTitles.Add(primaryJobTitle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = primaryJobTitle.PrimaryJobTitleId }, primaryJobTitle);
        }

        // DELETE: api/PrimaryJobTitles/5
        [ResponseType(typeof(PrimaryJobTitle))]
        public IHttpActionResult DeletePrimaryJobTitle(int id)
        {
            PrimaryJobTitle primaryJobTitle = db.PrimaryJobTitles.Find(id);
            if (primaryJobTitle == null)
            {
                return NotFound();
            }

            db.PrimaryJobTitles.Remove(primaryJobTitle);
            db.SaveChanges();

            return Ok(primaryJobTitle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrimaryJobTitleExists(int id)
        {
            return db.PrimaryJobTitles.Count(e => e.PrimaryJobTitleId == id) > 0;
        }
    }
}
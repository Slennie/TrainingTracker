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
    public class CertificatesController : ApiController
    {
        private VRMSTTDataContext db = new VRMSTTDataContext();

        // GET: api/Certificates
        public IHttpActionResult GetCertificates()
        {
            var resultSet = db.Certificates.OrderByDescending(c => c.CertExpirationDate).Select(c => new
            {
                c.CertificateId,
                c.UserId,
                c.Title,
                c.CertDateIssued,
                c.CertExpirationDate,
                c.IssuedBy
            });
            return Ok(resultSet);
        }

        // GET: api/Certificates/5
        [ResponseType(typeof(Certificate))]
        public IHttpActionResult GetCertificate(int id)
        {
            Certificate Certificate = db.Certificates.Find(id);
            if (Certificate == null)
            {
                return NotFound();
            }
                var resultSet = new
            {
                Certificate.CertificateId,
                Certificate.Title,
                Certificate.IssuedBy,
                Certificate.CertDateIssued,
                Certificate.CertExpirationDate,
                CertificateItems = Certificate.CertificateItems.Select(ci => new
                {
                    ci.CertificateItemId,
                    ci.Name,
                    ci.Url,
                    ci.CreatedDate
                })
            };
            return Ok(resultSet);
        }

        // PUT: api/Certificates/5
        // Certificate Graduation Table???
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCertificate(int id, Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != certificate.CertificateId)
            {
                return BadRequest();
            }

            var dbCertificate = db.Certificates.Find(id);

            dbCertificate.CertDateIssued = certificate.CertDateIssued;
            dbCertificate.CertExpirationDate = certificate.CertExpirationDate;
            dbCertificate.Title = certificate.Title;
            dbCertificate.IssuedBy = certificate.IssuedBy;

            db.Entry(dbCertificate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateExists(id))
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

        // POST: api/Certificates
        [ResponseType(typeof(Certificate))]
        public IHttpActionResult PostCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Certificates.Add(certificate);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = certificate.CertificateId }, certificate);
        }

        // DELETE: api/Certificates/5
        [ResponseType(typeof(Certificate))]
        public IHttpActionResult DeleteCertificate(int id)
        {
            Certificate certificate = db.Certificates.Find(id);
            if (certificate == null)
            {
                return NotFound();
            }

            db.Certificates.Remove(certificate);
            db.SaveChanges();

            return Ok(certificate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CertificateExists(int id)
        {
            return db.Certificates.Count(e => e.CertificateId == id) > 0;
        }
    }
}
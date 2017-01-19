using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using VRMSTT.Api.Infrastructure;
using VRMSTT.Api.Models;

namespace VRMSTT.Api.Controllers
{
    public class CertificateController : ApiController
    {

        private VRMSTTDataContext db;

        //Get: api/certificates
        public IHttpActionResult GetCertificates()
        {
            return Ok(db.Certificates);
        }


        //Get: api/certificates/5
        [ResponseType(typeof(Certificate))]
        public IHttpActionResult GetCertificate(int id)
        {
            Certificate Certificate = db.Certificates.Find(id);
            if (Certificate == null)
            {
                return NotFound();
            }
            return Ok(new
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
            });
        }

        //POST: api/certificates/5
       // [Authorize(Roles = "Admin, User")]
        [ResponseType(typeof(Certificate))]
        public IHttpActionResult PostCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState);
            }

            var certificate = Certificate.Identity.Name
        }

    } //SCOPE
}

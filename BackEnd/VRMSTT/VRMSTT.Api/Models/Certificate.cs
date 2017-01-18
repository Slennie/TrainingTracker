using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRMSTT.Api.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }

        public DateTime CertDateIssued { get; set; }
        public DateTime? CertExpirationDate { get; set; }

        public string IssuedBy { get; set; }

        //Navigation Properties

        public virtual ICollection<CertificateItem> CertificateItems { get; set; }
        public virtual User User { get; set; }


    }
}
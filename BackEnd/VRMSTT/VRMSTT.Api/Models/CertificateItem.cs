using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRMSTT.Api.Models
{
    public class CertificateItem
    {
        public int CertificateItemId { get; set; }
        public int? CertificateId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }

        //Navigational Properties

        public virtual Certificate Certificate { get; set; }

    }
}
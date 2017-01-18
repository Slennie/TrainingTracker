using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRMSTT.Api.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string UserId { get; set; }
        public string Instructor { get; set; }
        public string CourseTitle { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string location { get; set; }
        public bool? PPERequired { get; set; }
        public string MaterialsNeeded { get; set; }
        public string SpecialInstructions { get; set; }
        public bool? AvailableOnline { get; set; }
        public string CertificateAwarded { get; set; }
        public int EMSCE { get; set; } //What is this? continuing education analytics Yes/NO?
        public DateTime? CertificateExpiration { get; set; }
        public string Cost { get; set; }
        public string PaymentOption { get; set; } //dropdown?
        public string HostingOrganization { get; set; } //dropdown?
        public string ReimbursingAgency { get; set; } //dropdown?
        public string LabOrLecture { get; set; } //dropdown?

        //Navigation Properties

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual User User { get; set; }

    }
}
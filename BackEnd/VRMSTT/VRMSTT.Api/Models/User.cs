using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRMSTT.Api.Models
{
    public class User : IdentityUser
    {
        public int DepartmentId { get; set; }
        public int? PrimaryJobTitleId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public string Rank { get; set; }
        public string Unit { get; set; }
        public string PrimaryShift { get; set; }
        public string Timezone { get; set; }
        public string DOB { get; set; }
        public string TempPreference { get; set; }
        public string SpeedPreference { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; }

        //Navigation Properties
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual Department Department { get; set; }
        public virtual PrimaryJobTitle PrimaryJobTitle { get; set; }
    }
}
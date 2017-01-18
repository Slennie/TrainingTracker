using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRMSTT.Api.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        // Navigation Properties

        public virtual ICollection<User> Users { get; set; }

    }
}
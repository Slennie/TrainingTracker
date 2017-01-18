using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRMSTT.Api.Models
{
    public class Enrollment
    {
        public int CourseId { get; set; }
        public string UserId { get; set; }

        //Navigation Properties

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRMSTT.Api.Models
{
    public class PrimaryJobTitle
    {
        public int PrimaryJobTitleId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }

        // Navigation Properties

        public virtual ICollection<User> Users { get; set; }
    }
}
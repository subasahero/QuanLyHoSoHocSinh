using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Role : IdentityRole<Guid>, IDateTracking
    {
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

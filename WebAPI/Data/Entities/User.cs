using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class User : IdentityUser<Guid>, IDateTracking
    {
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string Avatar { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Status = true;
        }

        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public string Avatar { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? Status { get; set; }
        public string Password { get; set; }

        public string FullNameAndRole { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class UserCreationViewModel
    {
        public UserCreationViewModel()
        {
            Status = true;
        }

        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string CreatedDate { get; set; }
        public bool? Status { get; set; }
        public Guid RoleId { get; set; }
    }
}

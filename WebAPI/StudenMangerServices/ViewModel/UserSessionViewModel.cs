using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class UserSessionViewModel
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string RoleName { get; set; }
        public string Avatar { get; set; }
    }
}

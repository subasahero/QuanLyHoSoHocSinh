using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class UserChangePasswordViewModel
    {
        public string userId { get; set; }
        public string currentPassword { get; set; }
        public string  newPassword { get; set; }
    }
}

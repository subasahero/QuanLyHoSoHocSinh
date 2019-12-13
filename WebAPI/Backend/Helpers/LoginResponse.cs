using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Helpers
{
    public class LoginResponse
    {
        public LoginResponse()
        {

        }

        public LoginResponse(bool status, string token, string message)
        {
            Status = status;
            Token = token;
            Message = message;
        }

        public string Token { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}

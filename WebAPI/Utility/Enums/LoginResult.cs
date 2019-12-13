using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Enums
{
    public enum LoginResult
    {
        Succeeded,
        IsLockedOut,
        Unauthorized,
        Incorrect
    }
}

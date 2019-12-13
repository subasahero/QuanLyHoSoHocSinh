using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IDateTracking
    {
        string CreatedDate { get; set; }
        string ModifiedDate { get; set; }
    }
}

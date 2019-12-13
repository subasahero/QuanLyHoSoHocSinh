using Data.Enum;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Grade : IDateTracking
    {
        public Guid Id { get; set; }
        public LevelEnum levelEnum { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}

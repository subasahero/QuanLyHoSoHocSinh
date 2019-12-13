using Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class GradeViewModel
    {
        public GradeViewModel()
        {
            Status = true;
        }
        public Guid? Id { get; set; }
        public LevelEnum levelEnum { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? Status { get; set; }
    }
}

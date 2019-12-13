using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class ChangeGradeViewModel
    {
        public List<Guid> studentsId { get; set; }
        public Guid? gradeId { get; set; }
    }
}

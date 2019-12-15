using Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class StudentScoreViewModel
    {
        public StudentScoreViewModel()
        {
            Status = true;
        }

        public Guid? Id { get; set; }
        public Guid? StudentId { get; set; }
        public float SemesterOneLevelSix { get; set; }
        public float SemesterTwoLevelSix { get; set; }
        public float SemesterOneLevelSeven { get; set; }
        public float SemesterTwoLevelSeven { get; set; }
        public float SemesterOneLevelEight { get; set; }
        public float SemesterTwoLevelEight { get; set; }
        public float SemesterOneLevelNine { get; set; }
        public float SemesterTwoLevelNine { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? Status { get; set; }

        public StudentViewModel StudentVM { get; set; }
    }
}

using Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class StudentScore
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public float SemesterOneLevelSix { get; set; }
        public float SemesterTwoLevelSix { get; set; }
        public float SemesterOneLevelSeven { get; set; }
        public float SemesterTwoLevelSeven { get; set; }
        public float SemesterOneLevelEight { get; set; }
        public float SemesterTwoLevelEight { get; set; }
        public float SemesterOneLevelNine { get; set; }
        public float SemesterTwoLevelNine { get; set; }
        public float AverageLevelSix { get; set; }
        public float AverageLevelSeven { get; set; }
        public float AverageLevelEight { get; set; }
        public float AverageLevelNine { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool Status { get; set; }

        public virtual Student Student { get; set; }
    }
}

using Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            Status = true;
        }

        public Guid? Id { get; set; }
        public Guid? GradeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string BirthLocate { get; set; }
        public string Talent { get; set; }
        public string DateGoShcool { get; set; }
        public Guid? CertificateId { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? Status { get; set; }

        public GradeViewModel GradeVM { get; set; }
        public StudentScoreViewModel StudentScoreVM { get; set; }
    }
}

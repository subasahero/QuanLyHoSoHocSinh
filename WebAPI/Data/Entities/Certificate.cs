using Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Certificate
    {
        public Guid Id { get; set; }
        public string SubjectCareer { get; set; }
        public CertificateVacational CertificateType { get;set;}
    }
}

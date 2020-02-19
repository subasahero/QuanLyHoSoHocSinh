using Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class CertificateViewModel
    {
        public Guid? Id { get; set; }
        public Guid? StudentId { get; set; }
        public string SubjectCareer { get; set; }
        public CertificateVacational CertificateType { get; set; }
    }
}

using Data.Enum;
using Microsoft.AspNetCore.Http;
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
        public string imageLink { get; set; }
        public string address { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? Status { get; set; }

        public GradeViewModel GradeVM { get; set; }
        public StudentScoreViewModel StudentScoreVM { get; set; }
        public List<DetailRewardViewModel> DetailRewardVM { get; set; }
        public List<DetailDisciplineViewModel> DetailDisciplineVM { get; set; }
        public IFormFile File { get; set; }
        //public CertificateViewModel CertificateVM { get; set; }

        public DiemLopSauViewModel DiemLopSauHK1VM { get; set; }
        public DiemLopBayViewModel DiemLopBayHK1VM { get; set; }
        public DiemLopTamViewModel DiemLopTamHK1VM { get; set; }
        public DiemLopChinViewModel DiemLopChinHK1VM { get; set; }

        public DiemLopSauViewModel DiemLopSauHK2VM { get; set; }
        public DiemLopBayViewModel DiemLopBayHK2VM { get; set; }
        public DiemLopTamViewModel DiemLopTamHK2VM { get; set; }
        public DiemLopChinViewModel DiemLopChinHK2VM { get; set; }
    }
}

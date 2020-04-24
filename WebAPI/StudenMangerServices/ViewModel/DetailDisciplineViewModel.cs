using Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class DetailDisciplineViewModel
    {
        public DetailDisciplineViewModel()
        {
            Status = true;
        }
        public Guid? Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? DisciplineId { get; set; }
        public string Reason { get; set; }
        public string Punishment { get; set; }
        public string DatePunish { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? Status { get; set; }

        public DisciplineViewModel Discipline { get; set; }
        public StudentViewModel StudentVM { get; set; }

        public LevelEnum levelEnum { get; set; }
    }
}

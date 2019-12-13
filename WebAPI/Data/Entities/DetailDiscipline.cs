using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class DetailDiscipline : IDateTracking
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid DisciplineId { get; set; }
        public string Reason { get; set; }
        public string Punishment { get; set; }
        public string DatePunish { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool Status { get; set; }

        public virtual Discipline Discipline { get; set; }
        public virtual Student Student { get; set; }
    }
}

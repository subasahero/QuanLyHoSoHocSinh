using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class DisciplineViewModel
    {
        public DisciplineViewModel()
        {
            Status = true;
        }

        public Guid? Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? Status { get; set; }
    }
}

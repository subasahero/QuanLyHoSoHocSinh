﻿using Data.Enum;
using Data.Interfaces;
using System;

namespace Data.Entities
{
    public class Student : IDateTracking
    {
        public Guid Id { get; set; }
        public Guid GradeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string BirthLocate { get; set; }
        public string Talent { get; set; }
        public string DateGoShcool { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string imageLink { get; set; }
        public string address { get; set; }
        public bool Status { get; set; }

        public virtual Grade Grade { get; set; }
        //public Certificate Certificate { get; set; }
    }
}

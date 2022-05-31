using System;
using System.Collections.Generic;

#nullable disable

namespace Class_Management_System.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public DateTime? StudentDob { get; set; }
        public string StudentGender { get; set; }
        public string PreviousSchoolName { get; set; }
        public int? ClassID { get; set; }
        public string StudentPassword { get; set; }
    }
}

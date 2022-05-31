using System;
using System.Collections.Generic;

#nullable disable

namespace Class_Management_System.Models
{
    public partial class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherGender { get; set; }
        public int? SubjectId { get; set; }
        public string TeacherPassword { get; set; }
    }
}

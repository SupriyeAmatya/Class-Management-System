using System;
using System.Collections.Generic;

#nullable disable

namespace Class_Management_System.Models
{
    public partial class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? TotalMarks { get; set; }
        public int? ClassID { get; set; }
    }
}

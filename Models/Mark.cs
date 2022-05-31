using System;
using System.Collections.Generic;

#nullable disable

namespace Class_Management_System.Models
{
    public partial class Mark
    {
        public int MarksId { get; set; }
        public int? SubjectId { get; set; }
        public int? StudentId { get; set; }
        public string TermName { get; set; }
        public int? MarksGotten { get; set; }
    }
}

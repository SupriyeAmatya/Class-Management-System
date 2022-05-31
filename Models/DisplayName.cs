using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class_Management_System.Models
{
    public class DisplayName
    {

        public int adminID { get; set; }

        public string AdminName { get; set; }
        public string AdminPassword { get; set; }

        public int CLassID { get; set; }
        public string ClassName { get; set; }

        public int MarksID { get; set; }

        public string TermName { get; set; }
        public int? MarksGotten { get; set; }
        public int StudentID { get; set; }

        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public DateTime StudentDOB { get; set; }
        public string StudentGender { get; set; }

        public string PreviousSchoolName { get; set; }
        public string StudentPassword { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int TotalMarks { get; set; }
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherAddress { get; set; }
        public string TeacherGender { get; set; }
        public string TeacherPassword { get; set; }
    }
}

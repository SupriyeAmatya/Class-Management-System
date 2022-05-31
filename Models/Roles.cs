using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class_Management_System.Models
{
    public class Roles
    {

        public int roleid { get; set; }
        public string username { get; set; }
        public string userpassword { get; set; }

        public string roletype { get; set; }
        public int adminID { get; set; }
        public int StudentID { get; set; }
        public int TeacherID { get; set; }
        
    }
}

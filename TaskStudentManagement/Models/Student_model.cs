using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskStudentManagement.Models
{
    public class Student_model
    {
        public int id { get; set; }
        public int Teacher_id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
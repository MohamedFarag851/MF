using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArmyTechTask.Models.View_Models
{
    public class StudentViewModel
    {

        public Student Student { get; set; }

        public Teacher Teacher { get; set; }

        public List<Governorate> Governorates { get; set; }

        public List<Neighborhood> Neighborhoods { get; set; }

        public List<Field> Fields { get; set; }

        public List<Teacher> Teachers { get; set; }

        public List<StudentTeacher> studentTeachers { get; set; }
    }
}
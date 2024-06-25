using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class StudentB
    {
        //public int StudentId;
        //public string StudentName;

        public int StudentId { get; set; }
        public string StudentName { set; get; }
        public Grade? Grade { set; get; }

        public StudentB[] Friends { get; set; }
    }
}

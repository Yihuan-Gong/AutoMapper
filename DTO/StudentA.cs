using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class StudentA
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public GradeA? Grade { set; get; }

        public StudentA[] Friends { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test 1
            var mapper1 = new Mapper<GradeA, GradeB>();
            var gradeB = mapper1.Map(GradeA.B);


            // Test 2
            var studentA = new StudentA
            {
                Id = 1,
                Name = "Alice",
                Grade = GradeA.A,
                Friends = new StudentA[]
                {
                    new StudentA
                    {
                        Id = 2,
                        Name = "Bob",
                        Grade = GradeA.B,
                    },

                    new StudentA
                    {
                        Id = 3,
                        Name = "William",
                    },
                }
            };

            var mapper = new Mapper<StudentA, StudentB>();
            mapper.ForMember(x => x.Id, y => y.StudentId)
                  .ForMember(x => x.Name, y => y.StudentName);

            StudentB studentB = mapper.Map(studentA);



            Console.ReadKey();
        }
    }
}

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

            object obj = "Hello";
            Console.WriteLine(obj.GetType().Name);

            var studentA = new StudentA
            {
                Id = 1,
                Name = "Alice",
                Grade = "B",
                Friends = new StudentA[]
                {
                    new StudentA
                    {
                        Id = 2,
                        Name = "Bob",
                        Grade = "A",
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

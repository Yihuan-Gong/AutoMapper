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
            var studentA = new StudentA
            {
                Id = 1,
                Name = "Alice",
            };

            var mapper = new Mapper<StudentA, StudentB>();
            mapper.ForMember(x => x.Id, y => y.StudentId)
                  .ForMember(x => x.Name, y => y.StudentName);


            StudentB studentB = mapper.Map(studentA);

            Console.ReadKey();
        }
    }
}

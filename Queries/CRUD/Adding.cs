using Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6.CRUD
{
    class Adding
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            var course = new Course
            {
                Name = "new course",
                Description = "new descr",
                FullPrice = 19.95f,
                Level = 1,
                AuthorId = 1
            };

            context.Courses.Add(course);
            context.SaveChanges();
            
        }
    }
}

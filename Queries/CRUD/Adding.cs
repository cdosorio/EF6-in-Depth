using Models;
using System.Linq;

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
                AuthorId = 1, //using FK properties, better for web apps
                //Author = context.Authors.Single(a => a.Id == 1) //Using an existing object in context
            };

            context.Courses.Add(course);
            context.SaveChanges();            
        }
    }
}

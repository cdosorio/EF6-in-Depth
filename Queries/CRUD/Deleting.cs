using Models;
using System.Linq;
using System.Data.Entity;


namespace EF6.CRUD
{
    class Deleting
    {
        static void Main(string[] args)
        {
            //WithCascadeDelete();
            WithoutCascadeDelete();
        }

        private static void WithCascadeDelete()
        {
            //courses and tags
            var context = new PlutoContext();

            var course = context.Courses.Find(6); //Single(c => c.Id == 6)
            context.Courses.Remove(course);

            context.SaveChanges();
        }
               
        private static void WithoutCascadeDelete()
        {
            //authors and courses
            var context = new PlutoContext();

            //Eager load para sus cursos
            var author = context.Authors.Include(a => a.Courses).Single(a => a.Id == 2);
            context.Courses.RemoveRange(author.Courses);
            context.Authors.Remove(author);

            context.SaveChanges();
        }
    }
}

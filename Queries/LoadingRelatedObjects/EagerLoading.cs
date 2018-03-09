
using EF6;
using System;
using System.Data.Entity;
using System.Linq;

namespace LoadingRelated
{
    /// <summary>
    /// Incluye se convierte en JOIN
    /// </summary>
    class EagerLoading
    {
        static void Main(string[] args)
        {
            OneLevel();
            Console.ReadKey();
        }

        private static void OneLevel()
        {
            var context = new PlutoContext();

            //MSDN way
            //var courses = context.Courses.Include("Author").ToList();

            //Mosh way
            var courses = context.Courses.Include(c => c.Author).ToList();

            foreach (var course in courses)
            {
                Console.WriteLine("{0} by {1}", course.Name, course.Author.Name);
            }            
        }

        private static void MultipleLevels_SingleProp()
        {
            var context = new PlutoContext();                
            //one author per course
            var courses = context.Courses.Include(c => c.Author.Address).ToList();                        
        }

        private static void MultipleLevels_CollectionProp()
        {
            var context = new PlutoContext();
            //multiple tags per course
            var courses = context.Courses.Include(a => a.Tags.Select(t => t.Moderator)).ToList();            
        }


    }
}

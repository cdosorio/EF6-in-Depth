
using EF6;
using System;
using System.Linq;

namespace LoadingRelated
{
    /// <summary>
    /// Problema N+1 (aunque en este ejemplo no son 1+16 queries, ya que para los 16 cursos solo son 4 autores, 
    /// y como las queries se van guardando en cache del dbContext, solo se ejecutan 1+4
    /// </summary>
    class LazyLoading
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();
                        
            var courses = context.Courses.ToList();

            foreach (var course in courses)
            {
                Console.WriteLine("{0} by {1}", course.Name, course.Author.Name);
            }
            Console.ReadKey();
        }
                
    }
}

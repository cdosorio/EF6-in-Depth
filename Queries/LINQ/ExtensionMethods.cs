
using System;
using System.Linq;

namespace Queries
{
    class ExtensionMethods
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            //Filtering(context);
            //Ordering(context);
            //Projection(context);
            //Projection_SelectMany(context);
            //SetOperators(context);
            //Grouping(context);            
            Joining(context);
            //GroupJoin(context);
            //CrossJoin(context);
        }
           

        private static void Filtering(PlutoContext context)
        {
            var query = context.Courses.Where(c => c.Level == 1);                    
        }

        private static void Ordering(PlutoContext context)
        {
            var query = context.Courses
                    .Where(c => c.Level == 1)
                    .OrderBy(c => c.Name)
                    .ThenBy(c => c.Level);
        }

        private static void Projection(PlutoContext context)
        {
            var query = context.Courses
                    .Where(c => c.Level == 1)
                    .OrderBy(c => c.Name)
                    .ThenBy(c => c.Level)
                    .Select(c => new
                    {
                        CourseName = c.Name,
                        AuthorName = c.Author.Name
                    });
        }
                
        private static void Projection_SelectMany(PlutoContext context)
        {
            //To flaten an hierarchical list. 
            //De lo contrario se necesitaria doble foreach porque devolvería lista de lista.
            var tags = context.Courses
                    .Where(c => c.Level == 1)
                    .OrderBy(c => c.Name)
                    .ThenBy(c => c.Level)
                    .SelectMany(c => c.Tags);

            foreach (var t in tags)
            {          
                Console.WriteLine(t.Name);
            }
        }
                
        private static void SetOperators(PlutoContext context)
        {
            var tags = context.Courses
                    .Where(c => c.Level == 1)
                    .OrderBy(c => c.Name)
                    .ThenBy(c => c.Level)
                    .SelectMany(c => c.Tags)
                    .Distinct();

            foreach (var t in tags)
            {
                Console.WriteLine(t.Name);
            }
        }
        
        private static void Grouping(PlutoContext context)
        {
            var groups = context.Courses.GroupBy(c => c.Level);

            foreach (var group in groups)
            {
                Console.WriteLine("Key:" + group.Key);

                foreach (var course in group)
                    Console.WriteLine("\t" + course.Name);
            }

            Console.ReadKey();
        }
        
        private static void Joining(PlutoContext context)
        {
            context.Courses.Join(context.Authors,
                    c => c.AuthorId,
                    a => a.Id
                    , (course, author) => new
                    {
                        CourseName = course.Name,
                        AuthorName = author.Name
                    });
        }
        
        private static void GroupJoin(PlutoContext context)
        {
            //Equivalente a cuando en SQL hacemos LEFT JOIN con COUNT
            context.Authors.GroupJoin(context.Courses, a => a.Id, c => c.AuthorId, (author, courses) => new
            {
                AuthorName = author.Name,
                Courses = courses.Count()
            });            
        }

        private static void CrossJoin(PlutoContext context)
        {            
            //En extension method se implementa con SelectMany
            context.Authors.SelectMany(a => context.Courses, (author, course) => new
            {
                AuthorName = author.Name,
                CourseName = course.Name
            });
        }

        private static void Partitioning(PlutoContext context)
        {
            //util para paginar
            var courses = context.Courses.Skip(10).Take(10);
        }

        private static void ElementOperators(PlutoContext context)
        {
            //OrDefault evita la excepcion si no hay registros
            var firstCourse = context.Courses.OrderBy(c => c.Level).FirstOrDefault(c => c.FullPrice > 100);
            
            //No disponible en SQL SERVER
            var lastCourse = context.Courses.OrderBy(c => c.Level).Last();

            //OrDefault evita la excepcion si no hay registros
            //Multiples resultados: excepción. En ese caso usar FirstOrDefault
            var course = context.Courses.OrderBy(c => c.Level).SingleOrDefault(c => c.Id==1);

            //cuantificar
            bool allInLevel1 = context.Courses.All(c => c.Level == 1);
            bool anyInLevel1 = context.Courses.Any(c => c.Level == 1);

            //Aggregating
            int count = context.Courses.Count();

            int countInLeve1 = context.Courses.Count(c => c.Level == 1);
            int countInLeve1_ = context.Courses.Where(c => c.Level == 1).Count();

            var max = context.Courses.Max(c => c.FullPrice);
            var min = context.Courses.Min(c => c.FullPrice);
            var avg = context.Courses.Average(c => c.FullPrice);
            var sum = context.Courses.Sum(c => c.FullPrice);            
        }
    }
}

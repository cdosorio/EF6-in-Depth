
using System;
using System.Linq;

namespace Queries
{
    class LinqSyntax
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            //Comparision(context);
            //Filtering(context);
            //Ordering(context);
            //Projection(context);
            //Grouping(context);
            //AggregateFunctions(context);
            //Joining(context);
            //GroupJoin(context);
            CrossJoin(context);
        }

        private static void Comparision(PlutoContext context)
        {
            //LINQ syntax
            var query =
                from c in context.Courses
                where c.Name.Contains("c#")
                orderby c.Name
                select c;

            //foreach (var course in query)
            //    Console.WriteLine(course.Name);



            //Extension methods
            var courses = context.Courses
                .Where(c => c.Name.Contains("c#"))
                .OrderBy(c => c.Name);

            foreach (var course in query)
                Console.WriteLine(course.Name);
        }

        private static void Filtering(PlutoContext context)
        {
            var query =
                    from c in context.Courses
                    where c.Level == 1 && c.Author.Id == 1
                    select c;
        }

        private static void Ordering(PlutoContext context)
        {
            var query =
                    from c in context.Courses
                    where c.Author.Id == 1
                    orderby c.Level descending, c.Name
                    select c;
        }

        private static void Projection(PlutoContext context)
        {
            var query =
                    from c in context.Courses
                    where c.Level == 1
                    select new {
                        Name = c.Name,
                        Author = c.Author.Name
                    };
        }

        private static void Grouping(PlutoContext context)
        {
            var query =
                    from c in context.Courses
                    group c by c.Level
                    into g
                    select g;

            foreach (var group in query)
            {
                Console.WriteLine(group.Key);

                foreach (var course in group)
                    Console.WriteLine("\t{0}", course.Name);
            }

            Console.ReadKey();
        }

        private static void AggregateFunctions(PlutoContext context)
        {
            var query =
                    from c in context.Courses
                    group c by c.Level
                    into g
                    select g;

            foreach (var group in query)
            {
                Console.WriteLine("{0} ({1})", group.Key, group.Count());                                
            }

            Console.ReadKey();
        }

        private static void Joining(PlutoContext context)
        {
            //solo se necesita el inner join cuando dos entidades no tienen navigation property.
            //En este ejemplo en realidad no es necesario. 
            var query =
                    from c in context.Courses
                    join a in context.Authors on c.AuthorId equals a.Id
                    select new
                    {
                        CourseName = c.Name,                        
                        AuthorName = a.Name
                        //AuthorName = c.Author.Name
                    };            
        }
        
        private static void GroupJoin(PlutoContext context)
        {
            //Equivalente a cuando en SQL hacemos LEFT JOIN con COUNT
            var query =
                    from a in context.Authors
                    join c in context.Courses on a.Id equals c.AuthorId into g
                    select new
                    {
                        AuthorName = a.Name,
                        Courses = g.Count()
                    };

            foreach (var x in query)            
                Console.WriteLine("{0} ({1})", x.AuthorName, x.Courses);

            Console.ReadKey();
        }

        private static void CrossJoin(PlutoContext context)
        {            
            var query =
                    from a in context.Authors
                    from c in context.Courses 
                    select new
                    {
                        AuthorName = a.Name,
                        CourseName = c.Name
                    };

            foreach (var x in query)
                Console.WriteLine("{0} - {1}", x.AuthorName, x.CourseName);

            Console.ReadKey();
        }
    }
}

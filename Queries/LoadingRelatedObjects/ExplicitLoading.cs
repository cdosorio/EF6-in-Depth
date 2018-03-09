
using EF6;
using System;
using System.Data.Entity;
using System.Linq;

namespace LoadingRelated
{
    /// <summary>
    /// Ventajas sobre Eager Loading: 
    /// 1) simplifica las queries con muchos JOIN producto de muchos Include.
    /// 2) Permite aplicar filtros
    /// Desventaja: mas roundtrip a la BD (separate queries)
    /// </summary>
    class ExplicitLoading
    {
        static void Main(string[] args)
        {
            //LoadRelatedToOneEntity();
            LoadRelatedToMultipleEntities();
        }

        private static void LoadRelatedToOneEntity()
        {
            var context = new PlutoContext();

            var author = context.Authors.Single(a => a.Id == 1);
                        
            //Ademas se muestra como aplicar Filtro (FullPrice)
            context.Courses.Where(c => c.AuthorId == author.Id && c.FullPrice == 0).Load();
            Console.WriteLine("Cantidad={0}", author.Courses.Count);

            foreach (var course in author.Courses)
            {
                Console.WriteLine("{0}", course.Name);
            }

            Console.ReadKey();
        }
              
        private static void LoadRelatedToMultipleEntities()
        {
            var context = new PlutoContext();

            var authors = context.Authors.ToList();
            var authorIds = authors.Select(a => a.Id);

            //devuelve los cursos que están en cierta lista de autores 
            //Equivalente a usar el Operador IN de SQL
            context.Courses.Where(c => authorIds.Contains(c.AuthorId) && c.FullPrice == 0).Load();
            
            foreach (var a in authors)
            {
                Console.WriteLine(a.Name);
                foreach (var c in a.Courses)
                    Console.WriteLine("\t{0}\tPrice: {1}", c.Name, c.FullPrice);
            }

            Console.ReadKey();
        }

    }
}

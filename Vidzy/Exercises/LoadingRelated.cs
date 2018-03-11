
using System.Data.Entity;
using System;
using System.Linq;
using Vidzy;

namespace Vidzy
{
    /// <summary>
    /// Lazy Loading no recomendado para WEB
    /// Explicit Loading para simplicar queries muy complejas de Eager Loading, o cargar data condicionalmente
    /// </summary>
    class LoadingRelated
    {
        static void Main(string[] args)
        {
            //LazyLoading();
            //EagerLoading();
            ExplicitLoading();

            Console.ReadKey();
        }

        /// <summary>
        /// To enable lazy loading, you need to declare navigation properties as virtual. 
        /// </summary>
        private static void LazyLoading()
        {
            var context = new VidzyContext();

            var videos = context.Videos.ToList();

            foreach (var v in videos)
            {
                Console.WriteLine("{0} ({1})", v.Name, v.Genre.Name);
            }
        }


        private static void EagerLoading()
        {
            var context = new VidzyContext();

            //Include
            var videosWithGenres = context.Videos.Include(v => v.Genre).ToList();

            foreach (var v in videosWithGenres)
            {
                Console.WriteLine("{0} ({1})", v.Name, v.Genre.Name);
            }
        }

        private static void ExplicitLoading()
        {
            var context = new VidzyContext();
            var videos = context.Videos.ToList();

            //Explicit
            context.Genres.Load();

            foreach (var v in videos)
            {
                Console.WriteLine("{0} ({1})", v.Name, v.Genre.Name);
            }
        }
    }
}

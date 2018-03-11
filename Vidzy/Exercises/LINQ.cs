using System;
using System.Collections.Generic;
using System.Linq;

namespace Vidzy
{
    class LINQ
    {
        static void Main(string[] args)
        {
            var context = new VidzyContext();

            //Exercises section 6
            //ActionMoviesSortedByName(context);
            //GoldDramaSortedByReleaseDate(context);
            //ProjectedToAnonType(context);
            //GroupedByClassification(context);
            //ClassificationsWithTotals(context);
            GenresWithTotals(context);


            Console.ReadKey();
        }

        private static void ActionMoviesSortedByName(VidzyContext context)
        {
            var query = context.Videos
                    .Where(v => v.Genre.Name == "Action")
                    .OrderBy(v => v.Name);

            foreach (var v in query)
            {
                Console.WriteLine(v.Name);
            }
        }

        private static void GoldDramaSortedByReleaseDate(VidzyContext context)
        {
            var query = context.Videos
                    .Where(v => v.Classification == Classification.Gold && v.Genre.Name == "Drama")
                    .OrderByDescending(v => v.ReleaseDate);

            foreach (var v in query)
            {
                Console.WriteLine(v.Name);
            }
        }

        private static void ProjectedToAnonType(VidzyContext context)
        {
            var query = context.Videos
                    .Select(v => new
                    {
                        MovieName = v.Name,
                        Genre = v.Genre.Name
                    });

            foreach (var v in query)
            {
                Console.WriteLine(v.MovieName);
            }
        }

        private static void GroupedByClassification(VidzyContext context)
        {
            //Mediante projection
            var groups = context.Videos.GroupBy(v => v.Classification)
                 .Select(c => new
                 {
                     Classification = c.Key,
                     Vids = c.OrderBy(v => v.Name)
                 });
            
            foreach (var group in groups)
            {
                Console.WriteLine("Classification:" + group.Classification);

                foreach (var video in group.Vids)
                    Console.WriteLine("\t" + video.Name);
            }
        }

        private static void ClassificationsWithTotals(VidzyContext context)
        {
            var classifications = context.Videos
               .GroupBy(v => v.Classification)
               .Select(g => new
               {
                   Name = g.Key.ToString(),
                   VideosCount = g.Count()
               })
               .OrderBy(c => c.Name);            

            foreach (var c in classifications)
                Console.WriteLine("{0} ({1})", c.Name, c.VideosCount);
        }

        private static void GenresWithTotals(VidzyContext context)
        {
            //Equivalente a cuando en SQL hacemos LEFT JOIN con COUNT
            var genres = context.Genres.GroupJoin(context.Videos, g => g.Id, v => v.GenreId, (genre, videos) => new
            {
                GenreName = genre.Name,
                VideosCount = videos.Count()
            })
            .OrderByDescending(g => g.VideosCount); ;


            foreach (var g in genres)
                Console.WriteLine("{0} ({1})", g.GenreName, g.VideosCount);
        }
    }
}

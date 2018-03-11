using Models;
using System;
using System.Linq;

namespace EF6.CRUD
{
    class Updating
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            var course = context.Courses.Find(4, 1, 2); //Single(c => c.Id == 4)
            course.Name = "New name";
            course.AuthorId = 2;

            //Change tracker
            var entries = context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                entry.Reload();
                Console.WriteLine(entry.State);
            }

            context.SaveChanges();
            
        }
    }
}

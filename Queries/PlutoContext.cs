using System.Data.Entity;
using LINQ.EntityConfigurations;
using Models;

namespace EF6
{
    public class PlutoContext : DbContext
    {
        public PlutoContext()
            : base("name=PlutoContext")
        {
            //Recomendable para web apps. Reemplaza al uso de virtual
            this.Configuration.LazyLoadingEnabled = false;

            //cosoriob edit
            //cosoriob edit en branch para_PR
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseConfiguration());
        }
    }
}

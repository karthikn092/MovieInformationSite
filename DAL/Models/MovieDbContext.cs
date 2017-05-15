using System.Data.Entity;

namespace DAL.Models
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext() : base("MovieContext")
        {

        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Actor> Actor { get; set; }

        public DbSet<Producer> Producer { get; set; }

        public DbSet<MovieInfo> MovieInfo { get; set; }
    }
}

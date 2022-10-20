using Microsoft.EntityFrameworkCore;

namespace AspDotNetCoreApi6.Models
{
    public class MovieContext:DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options):base(options)
        {

        }

        public DbSet<Movie>? Movies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

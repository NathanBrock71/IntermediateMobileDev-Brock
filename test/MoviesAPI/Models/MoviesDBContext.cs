using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models
{
    public class MoviesDBContext : DbContext
    {
        public MoviesDBContext(DbContextOptions<MoviesDBContext> options)
        : base(options) { }

        public DbSet<Movie> Movies => Set<Movie>();
    }
}

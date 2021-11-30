using Microsoft.EntityFrameworkCore;
using OpetNet.Domain.Models;

namespace OpetNet.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Amizades> Amizades { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostsCurtidos> PostsCurtidos { get; set; }
    }
}

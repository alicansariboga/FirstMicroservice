using FirstMicroservice.Todos.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMicroservice.Todos.WebApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; } = default!;
    }
}
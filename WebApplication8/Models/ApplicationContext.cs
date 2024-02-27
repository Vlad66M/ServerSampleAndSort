using Microsoft.EntityFrameworkCore;

namespace WebApplication8.Models
{
    public class ApplicationContext : DbContext

    {
        public DbSet<Server> Servers { get; set; } = null!;


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

            //Database.EnsureDeleted();
            Database.EnsureCreated(); ///Проверяет сущствует база данных, если она не существует - создает базуданных.


        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace ActionFigureStore.Models
{
    public class TestDbContext : ActionFigureContext
    {
        public override DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ActionFigureStoreTest;integrated security = True");
        }
    }
}

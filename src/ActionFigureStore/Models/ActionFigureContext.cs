using Microsoft.EntityFrameworkCore;

namespace ActionFigureStore.Models
{
    public class ActionFigureContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ActionFigureStore;integrated security=True");
        }

        public ActionFigureContext(DbContextOptions<ActionFigureContext> options)
            : base(options)
        {
        }

        public ActionFigureContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
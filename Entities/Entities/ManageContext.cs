
using Entities.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities
{
    public class ManageContext : DbContext
    {
        public ManageContext(DbContextOptions<ManageContext> options) : base(options) { }

        public ManageContext() { }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configData = new ConfigData();
            modelBuilder.Entity<Category>().HasOne<Account>(m => m.Account).WithMany(a => a.Categories).HasForeignKey(m => m.IDAccount).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasOne<Category>(p => p.Category).WithMany(m => m.Products).HasForeignKey(p => p.IDCategory).OnDelete(DeleteBehavior.Cascade);
            configData.ConfigDataAccount(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}

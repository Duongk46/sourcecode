using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities.SeedData
{
    public class ConfigData
    {
        public void ConfigDataAccount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(new Account
            {
                ID = 1,
                Username = "admin",
                Password = "Admin@123",
            });
        }
    }
}

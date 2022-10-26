using Jídelníček.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Jídelníček.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasData(new User { UserId = 1, FirstName = "Miroslav", LastName = "Pluhař" });
            builder.Entity<Menu>().HasData(new Menu { MenuId = 1, Name = "Jídelníček", UserId = 1 });
            builder.Entity<Food>().HasData(new Food { FoodId = 1, Name = "Chleba s máslem", Kind = Kind.Snídaně });
            builder.Entity<Food>().HasData(new Food { FoodId = 2, Name = "Svíčková omáčka s houskovými knedlíky", Kind = Kind.Oběd });
            builder.Entity<Food>().HasData(new Food { FoodId = 3, Name = "Párek v rohlíku", Kind = Kind.Večeře });
            builder.Entity<User>()
                .HasMany(c => c.Menus)
                .WithOne(s => s.User)
                .IsRequired();
        }
    }
}

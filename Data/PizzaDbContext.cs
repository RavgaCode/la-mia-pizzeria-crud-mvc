using la_mia_pizzeria.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria.Data
{
    public class PizzaDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizzeria;Integrated Security=True;Encrypt=false\r\n");
        }
    }
}

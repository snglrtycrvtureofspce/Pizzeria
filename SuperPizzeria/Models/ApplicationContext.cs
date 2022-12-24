using SuperPizzeria.Classes;
using System.Data.Entity;
 
public class ApplicationContext : DbContext
{
    public ApplicationContext() : base("DefaultConnection" )
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Ingridient> Ingridients { get; set; }
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<CartUnit> CartUnits { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Sale> Sales { get; set; }

}


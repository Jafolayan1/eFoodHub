using eFoodHub.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eFoodHub.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        //Needed if any migration
        public ApplicationDbContext()
        {
        }

        //Configuration from AppSettings
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Needed for migration purpose only
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"data source=Shailendra\SqlExpress; initial catalog=ePizzaHubSite;integrated security=True;");

                //optionsBuilder.UseSqlServer(@"data source=JAY\SqlExpress; initial catalog=eFoodHub;persist security info=True;user id=sa;password=Aadmiral10@;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
using DMA_BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DMA_DAL
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		// Define your DbSets here
		public DbSet<Dish> Dishes { get; set; }
		public DbSet<Table> Tables { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderedItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Define relationships and foreign keys

			modelBuilder.Entity<OrderedItem>()
				.HasOne(oi => oi.Order)  // Specifies the Order navigation property
				.WithMany(o => o.OrderItems)  // Specifies the collection of OrderItems in Order
				.HasForeignKey(oi => oi.OrderId);  // Foreign key for OrderId in OrderItem

			modelBuilder.Entity<OrderedItem>()
				.HasOne(oi => oi.Dish)  // Specifies the Dish navigation property
				.WithMany()  // No reverse navigation property for Dish in OrderItem
				.HasForeignKey(oi => oi.DishId);  // Foreign key for DishId in OrderItem
		}
	}
}

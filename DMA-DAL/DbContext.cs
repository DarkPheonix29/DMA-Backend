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

			// OrderedItem -> Order
			modelBuilder.Entity<OrderedItem>()
				.HasOne(oi => oi.Order)
				.WithMany(o => o.OrderItems)
				.HasForeignKey(oi => oi.OrderId);

			// OrderedItem -> Dish
			modelBuilder.Entity<OrderedItem>()
				.HasOne(oi => oi.Dish)
				.WithMany()
				.HasForeignKey(oi => oi.DishId);

			// Order -> Table
			modelBuilder.Entity<Order>()
				.HasOne<Table>()
				.WithMany()
				.HasForeignKey(o => o.TableId);
		}
	}
}

using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
	public class StoreDbContext : DbContext
	{
		public DbSet<Product> Products => Set<Product>();

		public StoreDbContext(DbContextOptions<StoreDbContext> options)
			:base(options) { }
	}
}

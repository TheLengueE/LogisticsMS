using Microsoft.EntityFrameworkCore;

namespace LogisticsMS.Models
{
	public class SqlContext : DbContext
	{
		public DbSet<UserRole> UserRoles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

			var configuration = builder.Build();

			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}
	}
}

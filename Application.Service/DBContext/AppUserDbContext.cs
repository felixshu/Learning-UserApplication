using System.Data.Entity;
using GenericRepository;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Pattern.DataContext;
using User.Data.Mapping;
using User.Data.Model;

namespace Application.Service.DBContext
{
	public class AppUserDbContext : DataContext
	{
		public AppUserDbContext()
			: base("AppUserDbContext")
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<InventoryItem> InventoryItems { get; set; }
		public DbSet<Labor> Labors { get; set; }
		public DbSet<Part> Parts { get; set; }
		public DbSet<ServiceItem> ServiceItems { get; set; }
		public DbSet<WorkOrder> WorkOrders { get; set; }

		public static AppUserDbContext Create()
		{
			return new AppUserDbContext();
		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new CategoryConfiguration());
			modelBuilder.Configurations.Add(new CustomerConfiguration());
			modelBuilder.Configurations.Add(new InventoryConfiguration());
			modelBuilder.Configurations.Add(new LaborConfiguration());
			modelBuilder.Configurations.Add(new PartConfiguration());
			modelBuilder.Configurations.Add(new ServiceItemConfiguration());
			modelBuilder.Configurations.Add(new WorkOrderConfiguration());
			modelBuilder.Configurations.Add(new AppUserConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
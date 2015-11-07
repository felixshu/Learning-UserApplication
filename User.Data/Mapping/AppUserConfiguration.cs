using System.Data.Entity.ModelConfiguration;
using User.Data.Model;

namespace User.Data.Mapping
{
	public class AppUserConfiguration:EntityTypeConfiguration<AppUser>
	{
		public AppUserConfiguration()
		{
			Property(p => p.FirstName).HasMaxLength(15).IsOptional();
			Property(p => p.LastName).HasMaxLength(15).IsOptional();
		}
	}
}
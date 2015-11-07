using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using User.Data.Model;

namespace User.Data.Mapping
{
	public class WorkOrderConfiguration:EntityTypeConfiguration<WorkOrder>
	{
		public WorkOrderConfiguration()
		{
			Property(p => p.OrderDateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
			Property(p => p.Description).HasMaxLength(256).IsOptional();
			Property(p => p.TotalDue).HasPrecision(18, 2).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
			Property(p => p.CertificationRequirement).HasMaxLength(120).IsOptional();
		}
	}
}
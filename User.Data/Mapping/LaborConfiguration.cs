using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using User.Data.Model;

namespace User.Data.Mapping
{
	public class LaborConfiguration:EntityTypeConfiguration<Labor>
	{
		public LaborConfiguration()
		{
			Property(p => p.ServiceItemCode)
				.HasMaxLength(15)
				.IsRequired()
				.HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_Labor",2) {IsUnique = true}));

			Property(p=>p.WorkOrderId).HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_Labor",1) { IsUnique = true }));

			Property(p => p.ServiceItemName).HasMaxLength(80).IsRequired();

			Property(p => p.LaborHours).HasPrecision(18, 2);

			Property(p => p.Rate).HasPrecision(18, 2);

			Property(p => p.Total).HasPrecision(18, 2).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

			Property(p => p.Notes).HasMaxLength(140).IsOptional();
		}
	}
}
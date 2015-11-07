using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using User.Data.Model;

namespace User.Data.Mapping
{
	public class ServiceItemConfiguration:EntityTypeConfiguration<ServiceItem>
	{
		public ServiceItemConfiguration()
		{
			HasKey(p => p.ServiceItemId);
			Property(p => p.ServiceItemId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

			Property(p => p.ServiceItemCode)
				.HasMaxLength(15)
				.IsRequired()
				.HasColumnAnnotation(IndexAnnotation.AnnotationName, 
				new IndexAnnotation(new IndexAttribute("IX_ServiceCode"){ IsUnique = true}));

			Property(p => p.ServiceItemName)
				.HasMaxLength(80)
				.IsRequired()
				.HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_ServiceName") {IsUnique = true}));

			Property(p => p.Rate).HasPrecision(18, 2);
		}
	}
}
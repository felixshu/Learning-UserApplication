using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using User.Data.Model;

namespace User.Data.Mapping
{
	public class CustomerConfiguration:EntityTypeConfiguration<Customer>
	{
		public CustomerConfiguration()
		{
			Property(p => p.AccountNumber)
				.HasMaxLength(8)
				.IsRequired()
				.HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_Customer_AccountName") {IsUnique = true}));

			Property(p => p.CompanyName).HasMaxLength(30).IsRequired();
			Property(p => p.Address).HasMaxLength(30).IsRequired();
			Property(p => p.City).HasMaxLength(15).IsRequired();
			Property(p => p.State).HasMaxLength(2).IsRequired();
			Property(p => p.ZipCode).HasMaxLength(10).IsRequired();
			Property(p => p.Phone).HasMaxLength(15).IsOptional();
		}
	}
}
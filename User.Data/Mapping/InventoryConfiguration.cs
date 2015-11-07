using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using User.Data.Model;

namespace User.Data.Mapping
{
	public class InventoryConfiguration:EntityTypeConfiguration<InventoryItem>
	{
		public InventoryConfiguration()
		{
			HasKey(p => p.InventoryItemId);
			Property(p => p.InventoryItemId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

			Property(p => p.InventoryItemCode)
				.HasMaxLength(15)
				.IsRequired()
				.HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_InventoryItemCode",2) {IsUnique = true}));

			Property(p => p.InventoryItemName)
				.HasMaxLength(80)
				.IsRequired()
				.HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_InventoryItemName",1) {IsUnique = true}));

			Property(p => p.UnitPrice).HasPrecision(18,2);

			HasRequired(p => p.Category).WithMany(p => p.InventoryItems).HasForeignKey(p => p.CategoryId);
		}
	}
}
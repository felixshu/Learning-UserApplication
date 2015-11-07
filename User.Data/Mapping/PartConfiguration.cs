using System.CodeDom;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using User.Data.Model;

namespace User.Data.Mapping
{
	public class PartConfiguration:EntityTypeConfiguration<Part>
	{
		public PartConfiguration()
		{
			Property(p => p.InventroyItemName)
				.HasMaxLength(80)
				.IsRequired();

			Property(p => p.InventoryItemCode)
				.HasMaxLength(15)
				.IsRequired()
				.HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_Part_InventoryName",2) {IsUnique = true}));

			Property(p => p.WorkOrderId)
				.HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_Part_WorkOrderId",1) {IsUnique = true}));

			Property(p => p.UnitPrice).HasPrecision(18, 2);

			Property(p => p.Total).HasPrecision(18, 2).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

			Property(p => p.Notes).HasMaxLength(150).IsOptional();
		}
	}
}
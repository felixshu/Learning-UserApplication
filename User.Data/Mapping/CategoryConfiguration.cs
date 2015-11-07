using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using User.Data.Model;

namespace User.Data.Mapping
{
	public class CategoryConfiguration : EntityTypeConfiguration<Category>
	{
		public CategoryConfiguration()
		{
			Property(c => c.CategoryName)
				.HasMaxLength(20)
				.IsRequired()
				.HasColumnAnnotation(IndexAnnotation.AnnotationName,
					new IndexAnnotation(new IndexAttribute("IX_CategoryName") {IsUnique = true}));

			HasRequired(p => p.Category1)
				.WithMany(p => p.CategoryChidren)
				.HasForeignKey(p => p.ParentId);
		}
	}
}
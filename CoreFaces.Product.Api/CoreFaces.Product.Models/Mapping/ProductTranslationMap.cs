using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Product.Models.Domain;

namespace CoreFaces.Product.Models.Mapping
{
    public class ProductTranslationMap
    {
        public ProductTranslationMap(EntityTypeBuilder<ProductTranslation> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.CreatedUserId).IsRequired();
            entityBuilder.Property(t => t.UpdatedUserId).IsRequired();
            entityBuilder.Property(t => t.ProductId).IsRequired();
            entityBuilder.Property(t => t.Language).IsRequired();
            entityBuilder.Property(t => t.IsDefault);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Title);
            entityBuilder.Property(t => t.Description);
            entityBuilder.Property(t => t.Content);
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();
        }
    }
}

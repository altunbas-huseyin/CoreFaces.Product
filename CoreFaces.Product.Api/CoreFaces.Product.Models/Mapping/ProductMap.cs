using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CoreFaces.Product.Models.Mapping
{
    public class ProductMap
    {
        public ProductMap(EntityTypeBuilder<CoreFaces.Product.Models.Domain.Product> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.CreatedUserId).IsRequired();
            entityBuilder.Property(t => t.UpdatedUserId).IsRequired();
            entityBuilder.Property(t => t.ParentId).IsRequired();
            entityBuilder.Property(t => t.StockCode);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.BrandName).IsRequired();
            entityBuilder.Property(t => t.Price).IsRequired();
            entityBuilder.Property(t => t.Vat);
            entityBuilder.Property(t => t.Currency);
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();

            //Uniq Index
            entityBuilder.HasIndex(t => new { t.UserId, t.Name }).IsUnique();
        }
    }
}

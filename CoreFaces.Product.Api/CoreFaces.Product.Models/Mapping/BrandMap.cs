using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Product.Models.Domain;

namespace CoreFaces.Product.Models.Mapping
{
    public class BrandMap
    {
        public BrandMap(EntityTypeBuilder<Brand> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.CreatedUserId).IsRequired();
            entityBuilder.Property(t => t.UpdatedUserId).IsRequired();
            entityBuilder.Property(t => t.ApiUserId).IsRequired();
            entityBuilder.HasKey(t => t.ParentId);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Description).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();
            //Uniq Index
            entityBuilder.HasIndex(t => new { t.ApiUserId, t.Name }).IsUnique();
        }
    }
}

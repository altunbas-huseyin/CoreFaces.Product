using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Product.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Mapping
{

    public class ProductCategoryTranslationMap
    {
        public ProductCategoryTranslationMap(EntityTypeBuilder<ProductCategoryTranslation> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.CreatedUserId).IsRequired();
            entityBuilder.Property(t => t.UpdatedUserId).IsRequired();
            entityBuilder.Property(t => t.ProductCategoryId).IsRequired();
            entityBuilder.Property(t => t.Language).IsRequired();
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Description).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();
        }
    }
}

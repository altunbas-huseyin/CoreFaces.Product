using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Product.Models.Domain;

namespace CoreFaces.Product.Models.Mapping
{
    public class TestMap
    {
        public TestMap(EntityTypeBuilder<Test> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.FirstName).IsRequired();
            entityBuilder.Property(t => t.StatusId).IsRequired();
        }
    }
}

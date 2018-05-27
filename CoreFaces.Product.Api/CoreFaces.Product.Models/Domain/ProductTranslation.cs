using CoreFaces.Helper;
using System;

namespace CoreFaces.Product.Models.Domain
{
    public class ProductTranslation : EntityBase
    {
        public Guid CreatedUserId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid UpdatedUserId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid ProductId { get; set; }
        public Enums.Language Language { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}

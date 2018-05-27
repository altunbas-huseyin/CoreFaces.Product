using CoreFaces.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Domain
{
    public class ProductCategoryTranslation : EntityBase
    {
        public Guid CreatedUserId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid UpdatedUserId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid ProductCategoryId { get; set; }
        public Enums.Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

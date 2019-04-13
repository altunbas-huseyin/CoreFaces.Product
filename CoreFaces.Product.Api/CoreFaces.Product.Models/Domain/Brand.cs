using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Domain
{
    public class Brand : EntityBase
    {
        /// <summary>
        /// ApiUserId identity-admin.kuaforx.com'da oluşturulan userId'dir
        /// </summary>
        public Guid ApiUserId { get; set; }
        public Guid CreatedUserId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid UpdatedUserId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid ParentId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

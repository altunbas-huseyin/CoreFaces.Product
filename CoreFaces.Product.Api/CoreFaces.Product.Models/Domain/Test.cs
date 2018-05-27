using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Domain
{
    public class Test : EntityBase
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
    }
}

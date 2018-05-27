using CoreFaces.Product.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Models
{
    public class ProductView
    {
        public Domain.Product Product { get; set; }
        public List<ProductTranslation> ProductTranslationList { get; set; }
    }
}

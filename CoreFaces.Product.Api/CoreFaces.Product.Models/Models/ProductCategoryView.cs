using CoreFaces.Product.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Models
{
    public class ProductCategoryView : ProductCategory
    {
        public List<ProductCategoryTranslation> ProductCategoryTranslationList { get; set; }
    }
}

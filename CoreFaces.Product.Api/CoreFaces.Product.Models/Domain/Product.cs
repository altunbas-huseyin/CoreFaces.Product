using CoreFaces.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Domain
{
    public class Product : EntityBase
    {

        public Guid UserId { get; set; }
        public Guid ParentId { get; set; }
        public Guid CreatedUserId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid UpdatedUserId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid VendorId { get; set; }
        public string StockCode { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Ürün Fiyatı
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Para birimi
        /// </summary>
        public Enums.Currency Currency { get; set; }
        /// <summary>
        /// Kdv Oranı yani vergi oranı
        /// </summary>
        public decimal Vat { get; set; }
    }
}

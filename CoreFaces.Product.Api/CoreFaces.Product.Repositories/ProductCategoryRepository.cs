using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Product.Models;
using CoreFaces.Product.Models.Domain;
using CoreFaces.Product.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFaces.Licensing;

namespace CoreFaces.Product.Repositories
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
    {
        Models.Domain.ProductCategory GetById(Guid id, Guid apiUserId);
        bool Delete(Guid id, Guid apiUserId);
    }
    public class ProductCategoryRepository : Licence, IProductCategoryRepository
    {
        private readonly ProductDatabaseContext _productDatabaseContext;

        public ProductCategoryRepository(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Product", iHttpContextAccessor, productSettings.Value.ProductLicenseDomain, productSettings.Value.ProductLicenseKey)
        {
            _productDatabaseContext = productDatabaseContext;
        }


        public ProductCategory GetById(Guid id, Guid apiUserId)
        {
            ProductCategory model = _productDatabaseContext.Set<ProductCategory>().Where(p => p.Id == id && p.ApiUserId == apiUserId).FirstOrDefault();
            return model;
        }

        public Guid Save(ProductCategory productCategory)
        {
            _productDatabaseContext.Add(productCategory);
            _productDatabaseContext.SaveChanges();
            return productCategory.Id;
        }


        public bool Delete(Guid id, Guid apiUserId)
        {
            ProductCategory obj = this.GetById(id, apiUserId);
            _productDatabaseContext.Remove(obj);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(ProductCategory productCategory)
        {
            _productDatabaseContext.Update(productCategory);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

}

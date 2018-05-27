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
    public interface IProductCategoryTranslationRepository : IBaseRepository<ProductCategoryTranslation>
    {
        bool Delete(Guid id);

        ProductCategoryTranslation GetById(Guid id);
    }
    public class ProductCategoryTranslationRepository : Licence, IProductCategoryTranslationRepository
    {
        private readonly ProductDatabaseContext _productDatabaseContext;

        public ProductCategoryTranslationRepository(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Product", iHttpContextAccessor, productSettings.Value.ProductLicenseDomain, productSettings.Value.ProductLicenseKey)
        {
            _productDatabaseContext = productDatabaseContext;
        }

        public ProductCategoryTranslation GetById(Guid id)
        {
            ProductCategoryTranslation model = _productDatabaseContext.Set<ProductCategoryTranslation>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(ProductCategoryTranslation productCategoryTranslation)
        {
            _productDatabaseContext.Add(productCategoryTranslation);
            _productDatabaseContext.SaveChanges();
            return productCategoryTranslation.Id;
        }

        public bool Delete(Guid id)
        {
            ProductCategoryTranslation productCategoryTranslation = this.GetById(id);
            _productDatabaseContext.Remove(productCategoryTranslation);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(ProductCategoryTranslation productCategoryTranslation)
        {
            _productDatabaseContext.Update(productCategoryTranslation);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public ProductCategoryTranslation GetById(Guid id, Guid apiUserId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id, Guid apiUserId)
        {
            throw new NotImplementedException();
        }
    }

}

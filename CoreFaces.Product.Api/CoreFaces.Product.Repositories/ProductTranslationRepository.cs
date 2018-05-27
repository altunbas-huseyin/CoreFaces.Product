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
    public interface IProductTranslationRepository : IBaseRepository<ProductTranslation>
    {
        List<ProductTranslation> GetByProductId(Guid productId);
        ProductTranslation GetById(Guid id);
        bool Delete(Guid id);

    }

    public class ProductTranslationRepository : Licence, IProductTranslationRepository
    {
        private readonly ProductDatabaseContext _productDatabaseContext;

        public ProductTranslationRepository(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Product", iHttpContextAccessor, productSettings.Value.ProductLicenseDomain, productSettings.Value.ProductLicenseKey)
        {
            _productDatabaseContext = productDatabaseContext;
        }

        public ProductTranslation GetById(Guid id)
        {
            ProductTranslation model = _productDatabaseContext.Set<ProductTranslation>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(ProductTranslation productTranslation)
        {
            _productDatabaseContext.Add(productTranslation);
            _productDatabaseContext.SaveChanges();
            return productTranslation.Id;
        }

        public bool Delete(Guid id)
        {
            ProductTranslation model = this.GetById(id);
            _productDatabaseContext.Remove(model);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(ProductTranslation productTranslation)
        {
            _productDatabaseContext.Update(productTranslation);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public List<ProductTranslation> GetByProductId(Guid productId)
        {
            List<ProductTranslation> model = _productDatabaseContext.Set<ProductTranslation>().Where(p => p.ProductId == productId).ToList();
            return model;

        }

        public ProductTranslation GetById(Guid id, Guid apiUserId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id, Guid apiUserId)
        {
            throw new NotImplementedException();
        }
    }

}

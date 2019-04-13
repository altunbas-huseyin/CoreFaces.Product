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
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        Models.Domain.Brand GetById(Guid id, Guid apiUserId);
        bool Delete(Guid id, Guid apiUserId);
    }
    public class BrandRepository : Licence, IBrandRepository
    {
        private readonly ProductDatabaseContext _productDatabaseContext;

        public BrandRepository(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Product", iHttpContextAccessor, productSettings.Value.ProductLicenseDomain, productSettings.Value.ProductLicenseKey)
        {
            _productDatabaseContext = productDatabaseContext;
        }


        public Brand GetById(Guid id, Guid apiUserId)
        {
            Brand model = _productDatabaseContext.Set<Brand>().Where(p => p.Id == id && p.ApiUserId == apiUserId).FirstOrDefault();
            return model;
        }

        public Guid Save(Brand model)
        {
            _productDatabaseContext.Add(model);
            _productDatabaseContext.SaveChanges();
            return model.Id;
        }


        public bool Delete(Guid id, Guid apiUserId)
        {
            Brand obj = this.GetById(id, apiUserId);
            _productDatabaseContext.Remove(obj);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(Brand model)
        {
            _productDatabaseContext.Update(model);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

}

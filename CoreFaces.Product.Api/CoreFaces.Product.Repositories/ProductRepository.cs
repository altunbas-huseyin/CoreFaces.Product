using Kendo.DynamicLinq;
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
    public interface IProductRepository : IBaseRepository<Models.Domain.Product>
    {
        Models.Domain.Product GetById(Guid id, Guid apiUserId);
        bool Delete(Guid id, Guid apiUserId);
        List<Models.Domain.Product> Get(Kendo.DynamicLinq.View filters);
    }

    public class ProductRepository : Licence, IProductRepository
    {
        private readonly ProductDatabaseContext _productDatabaseContext;

        public ProductRepository(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Product", iHttpContextAccessor, productSettings.Value.ProductLicenseDomain, productSettings.Value.ProductLicenseKey)
        {
            _productDatabaseContext = productDatabaseContext;
        }

        public Models.Domain.Product GetById(Guid id, Guid userId)
        {
            Models.Domain.Product model = _productDatabaseContext.Set<Models.Domain.Product>().Where(p => p.Id == id && p.UserId == userId).FirstOrDefault();
            return model;
        }

        public Guid Save(Models.Domain.Product product)
        {
            _productDatabaseContext.Add(product);
            _productDatabaseContext.SaveChanges();
            return product.Id;
        }

        public bool Delete(Guid id, Guid apiUserId)
        {
            Models.Domain.Product model = this.GetById(id, apiUserId);
            _productDatabaseContext.Remove(model);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(Models.Domain.Product product)
        {
            _productDatabaseContext.Update(product);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public List<Models.Domain.Product> Get(Kendo.DynamicLinq.View filters)
        {
            Filter Filter = null;
            if (filters.Filter != null)
            {
                Filter = filters.FieldTypeCheckAll(filters.Filter);
            }
            if (Filter != null)
            {
                filters.Filter = Filter;
            }

            List<Models.Domain.Product> result = (List<Models.Domain.Product>)_productDatabaseContext.Set<Models.Domain.Product>()
                      .OrderBy(p => p.Id) // EF requires ordering for paging
                   .ToDataSourceResult(filters.Take, filters.Skip, filters.Sort, filters.Filter, filters.Aggregates).Data;
            return result;
        }
    }

}

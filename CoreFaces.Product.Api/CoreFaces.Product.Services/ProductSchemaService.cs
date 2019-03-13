using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Product.Models.Models;
using CoreFaces.Product.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Services
{
    public interface IProductSchemaService
    {
        bool DropTables();
        bool EnsureCreated();
    }

    public class ProductSchemaService : IProductSchemaService
    {
        private readonly ProductDatabaseContext _productDatabaseContext;
        private readonly IProductSchemaRepository _productSchemaRepository;
        public ProductSchemaService(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _productDatabaseContext = productDatabaseContext;
            _productSchemaRepository = new ProductSchemaRepository(_productDatabaseContext, productSettings, iHttpContextAccessor);
        }

        public bool DropTables()
        {
            return _productSchemaRepository.DropTables();
        }

        public bool EnsureCreated()
        {
            return _productSchemaRepository.EnsureCreated();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Product.Models.Models;
using CoreFaces.Product.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Services
{
    public interface ISchemaService
    {
        bool DropTables();
        bool EnsureCreated();
    }

    public class SchemaService : ISchemaService
    {
        private readonly ProductDatabaseContext _productDatabaseContext;
        private readonly ISchemaRepository _schemaRepository;
        public SchemaService(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _productDatabaseContext = productDatabaseContext;
            _schemaRepository = new SchemaRepository(_productDatabaseContext, productSettings, iHttpContextAccessor);
        }

        public bool DropTables()
        {
            return _schemaRepository.DropTables();
        }

        public bool EnsureCreated()
        {
            return _schemaRepository.EnsureCreated();
        }
    }
}

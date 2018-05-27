using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
    public interface ISchemaRepository
    {
        bool DropTables();
        bool EnsureCreated();
    }

    public class SchemaRepository : Licence, ISchemaRepository
    {
        private readonly ProductDatabaseContext _productDatabaseContext;

        public SchemaRepository(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Product", iHttpContextAccessor, productSettings.Value.ProductLicenseDomain, productSettings.Value.ProductLicenseKey)
        {
            _productDatabaseContext = productDatabaseContext;
        }

        public bool DropTables()
        {
            int result = _productDatabaseContext.Database.ExecuteSqlCommand("DROP TABLE ProductCategoryTranslation; DROP TABLE ProductCategory; DROP TABLE ProductTranslation; DROP TABLE Product;  DROP TABLE Test;");
            if (result == 0)
                return true;
            else
                return false;
        }

        public bool EnsureCreated()
        {
            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)_productDatabaseContext.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();
            return true;
        }
    }

}

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
    public interface ITestRepository : IBaseRepository<Test>
    {
        Test GetById(Guid id);
        bool Delete(Guid id);

        Test GetByEmail(string email);
    }
    public class TestRepository : Licence, ITestRepository
    {
        private readonly ProductDatabaseContext _productDatabaseContext;

        public TestRepository(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Product", iHttpContextAccessor, productSettings.Value.ProductLicenseDomain, productSettings.Value.ProductLicenseKey)
        {
            _productDatabaseContext = productDatabaseContext;
        }

        public Test GetByEmail(string email)
        {
            Guid g = Guid.Parse("dfe32bbd-3e0e-4c79-ab17-5f3104296b7e");
            Test model = _productDatabaseContext.Set<Test>().Where(p => p.Id == g).FirstOrDefault();
            return model;
        }

        public Test GetById(Guid id)
        {
            Test model = _productDatabaseContext.Set<Test>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public Guid Save(Test test)
        {
            _productDatabaseContext.Add(test);
            _productDatabaseContext.SaveChanges();
            return test.Id;
        }


        public bool Delete(Guid id)
        {
            Test test = this.GetById(id);
            _productDatabaseContext.Remove(test);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public bool Update(Test test)
        {
            _productDatabaseContext.Update(test);
            int result = _productDatabaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public Test GetById(Guid id, Guid apiUserId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id, Guid apiUserId)
        {
            throw new NotImplementedException();
        }
    }

}

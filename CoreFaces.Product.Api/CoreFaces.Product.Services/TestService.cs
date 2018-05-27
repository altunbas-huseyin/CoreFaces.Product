using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Product.Models.Domain;
using CoreFaces.Product.Models.Models;
using CoreFaces.Product.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Services
{
    public interface ITestService : IBaseService<Test>
    {
        CoreFaces.Product.Models.Domain.Test GetById(Guid id);
        bool Delete(Guid id);
        Test GetByEmail(string email);
    }
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        public TestService(ProductDatabaseContext productDatabaseContext, IOptions<ProductSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _testRepository = new TestRepository(productDatabaseContext, productSettings, iHttpContextAccessor);
        }

        public Test GetByEmail(string email)
        {
            return _testRepository.GetByEmail(email);
        }

        public Test GetById(Guid id)
        {
            return _testRepository.GetById(id);
        }

        public Guid Save(Test test)
        {
            _testRepository.Save(test);
            return test.Id;
        }

        public bool Delete(Guid id)
        {
            return _testRepository.Delete(id);
        }

        public bool Update(Test test)
        {
            return _testRepository.Update(test);

        }
    }
}

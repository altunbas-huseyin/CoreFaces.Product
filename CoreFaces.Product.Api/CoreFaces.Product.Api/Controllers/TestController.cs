using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CoreFaces.Product.Services;
using CoreFaces.Product.Repositories;
using CoreFaces.Product.Models.Models;
using CoreFaces.Product.Models.Domain;

namespace CoreFaces.Product.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly ProductDatabaseContext _productDatabaseContext;
        private readonly ProductSettings _productSettings;
        public TestController(IOptions<ProductSettings> productSettings, ITestService testService, ProductDatabaseContext productDatabaseContext)
        {
            _productSettings = productSettings.Value;
            _testService = testService;
            _productDatabaseContext = productDatabaseContext;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post(Test test)
        {
            Guid insertId = _testService.Save(test);
        }
    }
}

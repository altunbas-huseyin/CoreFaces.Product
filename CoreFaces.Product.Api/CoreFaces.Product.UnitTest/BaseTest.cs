
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CoreFaces.Product.Repositories;
using CoreFaces.Product.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using CoreFaces.Product.Api;
using System.Net.Http.Headers;
using System;
using Microsoft.Extensions.Configuration;
using CoreFaces.Product.Models.Models;
using Castle.Core.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace CoreFaces.Product.UnitTest
{
    public class BaseTest
    {
        public ProductDatabaseContext _productDatabaseContext;
        public readonly ITestService _testService;
        public readonly IProductService _productService;
        public readonly IProductCategoryService _productCategoryService;
        public readonly IProductSchemaService productSchemaService;
        public readonly TestServer _server;
        public readonly HttpClient _client;
        public readonly IOptions<ProductSettings> _productSettings;
        public readonly IHttpContextAccessor iHttpContextAccessor;
        public BaseTest()
        {
            var serviceProvider = new ServiceCollection()
           //.AddEntityFrameworkSqlServer()
           //.AddEntityFrameworkNpgsql()
           //.AddE
           .AddTransient<ITestService, TestService>()
           .BuildServiceProvider();

            DbContextOptionsBuilder<ProductDatabaseContext> builderProduct = new DbContextOptionsBuilder<ProductDatabaseContext>();
            var connectionString = "server=localhost;userid=root;password=123456;database=Product;";
            builderProduct.UseMySql(connectionString);
            //.UseInternalServiceProvider(serviceProvider); //burası postgress ile sıkıntı çıkartmıyor, fakat mysql'de çalışmıyor test esnasında hata veriyor.

            _productDatabaseContext = new ProductDatabaseContext(builderProduct.Options);
            //_context.Database.Migrate();

            //Configuration
             iHttpContextAccessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext() };
            _productSettings = Options.Create(new ProductSettings()
            {
                FileUploadFolderPath = @"C:\Users\haltunbas\Documents\GitHub\ProductV2\Product.Api\Product.Api\wwwroot\upload\"
            });

            _testService = new TestService(_productDatabaseContext, _productSettings, iHttpContextAccessor);
            _productService = new ProductService(_productDatabaseContext, _productSettings, iHttpContextAccessor);
            _productCategoryService = new ProductCategoryService(_productDatabaseContext, _productSettings, iHttpContextAccessor);
            productSchemaService = new ProductSchemaService(_productDatabaseContext, _productSettings, iHttpContextAccessor);

            _server = new TestServer(new WebHostBuilder()
               .UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //LoginControllerTest controller = new LoginControllerTest();
            //Guid token = controller.GetToken().Result;
            //_client.DefaultRequestHeaders.Add("Token", token.ToString());
            _client.DefaultRequestHeaders.Add("Token", "00000000-0000-0000-0000-000000000000");
        }
    }
}

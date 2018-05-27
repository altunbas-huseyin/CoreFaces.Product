using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using CoreFaces.Product.Api.Controllers;
using CoreFaces.Product.Models.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreFaces.Product.UnitTest
{
    [TestClass]
    public class TestControllerTest : BaseTest
    {

        [TestMethod]
        public void TestControllerPost()
        {
            TestController testController = new TestController(_productSettings,_testService, _productDatabaseContext);
            var t = testController.Get();
            Test test = new Test { FirstName = "test-***45" };
            testController.Post(test);
        }

        [TestMethod]
        public async Task TestControllerGet()
        {
            LoginControllerTest login = new LoginControllerTest();
            Guid token = await login.GetToken();

            _client.DefaultRequestHeaders.Add("Token", token.ToString());
            var response = await _client.GetAsync("/api/test");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

        }
    }
}

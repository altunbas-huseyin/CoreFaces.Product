
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CoreFaces.Product.Api.ControllersV1;
using System.Collections.Generic;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using CoreFaces.Product.Models.Models;
using CoreFaces.Helper;

namespace CoreFaces.Product.UnitTest
{
    [TestClass]
    public class ProductsControllerTest : BaseTest
    {


        [TestMethod]
        public async Task KendoLinq()
        {
            ProductsController controller = new ProductsController(_productSettings, _productDatabaseContext, _productService, iHttpContextAccessor);
            List<Kendo.DynamicLinq.Sort> sort = new List<Kendo.DynamicLinq.Sort>();
            List<Kendo.DynamicLinq.Aggregator> Aggregator = new List<Kendo.DynamicLinq.Aggregator>();

            List<Kendo.DynamicLinq.Filter> filterList = new List<Kendo.DynamicLinq.Filter>();
            filterList.Add(new Kendo.DynamicLinq.Filter { Field = "Name", Operator = "eq", Value = "iphone 6", Logic = " and " });
            filterList.Add(new Kendo.DynamicLinq.Filter { Field = "Id", Operator = "eq", Value = Guid.Parse("65e0a829-1bd3-4d51-a009-4a56a4989971"), Logic = " and " });

            Kendo.DynamicLinq.Filter filter = new Kendo.DynamicLinq.Filter { Filters = filterList, Logic = " and " };
            Kendo.DynamicLinq.View kendoParams = new Kendo.DynamicLinq.View { Take = 0, Skip = 0, Aggregates = null, Filter = filter, Sort = null };
            var result = controller.Post(kendoParams);
        }

        [TestMethod]
        public async Task KendoLinqHttp()
        {

            List<Kendo.DynamicLinq.Sort> sort = new List<Kendo.DynamicLinq.Sort>();
            List<Kendo.DynamicLinq.Aggregator> Aggregator = new List<Kendo.DynamicLinq.Aggregator>();

            List<Kendo.DynamicLinq.Filter> filterList = new List<Kendo.DynamicLinq.Filter>();
            filterList.Add(new Kendo.DynamicLinq.Filter { Field = "Name", Operator = "eq", Value = "iphone 6", Logic = " and " });
            filterList.Add(new Kendo.DynamicLinq.Filter { Field = "Id", Operator = "eq", Value = Guid.Parse("65e0a829-1bd3-4d51-a009-4a56a4989971"), Logic = " and " });

            Kendo.DynamicLinq.Filter filter = new Kendo.DynamicLinq.Filter { Filters = filterList, Logic = " and " };
            Kendo.DynamicLinq.View filters = new Kendo.DynamicLinq.View { Take = 0, Skip = 0, Aggregates = null, Filter = filter, Sort = null };

            //Filter filter1 = filterList[0];
            //MyKendo.Filter  fillllterrr = new MyKendo.Filter { Field = "Id", Operator = "eq", Value = Guid.Parse("48f38841-7adf-4d00-9348-9df906f3a31c"), Logic = " and " };
            var ff =  JsonConvert.SerializeObject(filters);
            var response = await _client.PostAsync("/api/products", new StringContent(JsonConvert.SerializeObject(filters), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            CommonApiResponse<ProductView> model = JsonConvert.DeserializeObject<CommonApiResponse<ProductView>>(responseString);
        }

    }
}

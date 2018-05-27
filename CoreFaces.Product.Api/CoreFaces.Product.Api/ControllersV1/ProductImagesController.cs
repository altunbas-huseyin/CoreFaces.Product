using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kendo.DynamicLinq;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using CoreFaces.Product.Repositories;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Product.Services;
using CoreFaces.Product.Models.Models;
using CoreFaces.Helper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreFaces.Product.Api.ControllersV1
{
    [ApiVersion("1")]
    [Route("api/[controller]")]
    public class ProductImagesController : Controller
    {
        Jwt jwt = new Jwt();

        private readonly ProductDatabaseContext _productDatabaseContext;
        private readonly ProductService _productService;
        private readonly IOptions<ProductSettings> _productSettings;
        public ProductImagesController(IOptions<ProductSettings> productSettings, ProductDatabaseContext productDatabaseContext, IProductService productService, IHttpContextAccessor iHttpContextAccessor)
        {
            _productSettings = productSettings;
            _productDatabaseContext = productDatabaseContext;
            _productService = new ProductService(_productDatabaseContext, productSettings, iHttpContextAccessor);
        }

        [HttpPost]
        public async Task<CommonApiResponse<List<ProductView>>> Post([FromBody]Kendo.DynamicLinq.View filters)
        {

            List<ProductView> result = _productService.GetProductViewList(Guid.Parse("00000000-0000-0000-0000-000000000000"), filters);
            return CommonApiResponse<List<ProductView>>.Create(Response, System.Net.HttpStatusCode.OK, true, result, "");

        }



        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

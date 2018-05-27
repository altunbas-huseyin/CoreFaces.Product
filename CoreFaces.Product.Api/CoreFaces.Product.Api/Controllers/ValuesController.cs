using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using CoreFaces.Status.Repositories;
using CoreFaces.Status.Services;
using CoreFaces.Product.Repositories;
using CoreFaces.Product.Services;

namespace CoreFaces.Product.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ITestService _testService;
        private readonly IStatusService _iStatusService;
        public readonly ProductDatabaseContext _productDatabaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ValuesController(ITestService testService, ProductDatabaseContext productDatabaseContext, IHostingEnvironment hostingEnvironment, StatusDatabaseContext statusDatabaseContext, IStatusService iStatusService)
        {
            _iStatusService = iStatusService;
            _testService = testService;
            _productDatabaseContext = productDatabaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET api/values

        [HttpGet]
        public IEnumerable<string> Get()
        {
           //bool result = _iStatusService.EnsureCreated();
           var t = _iStatusService.GetAll();
            //Identity.Models.Users.UserLoginView  fff =  new Identity.Models.Users.UserLoginView { Email="altunbas.huseyin@gmail.com", Password="1111" };
            //Identity.Client.Client client = new Identity.Client.Client("http://identity.kuaforx.com/");
            //Identity.Models.Users.UserView ggg = AsyncHelpers.RunSync<Identity.Models.Users.UserView>(() => client.Login(fff));
            _testService.GetById(Guid.NewGuid());
            //UploadImage();
            return new string[] { "value1", "value2" };
        }

        //public async void UploadImage()
        //{
        //    FileClient fileClient = new FileClient(_hostingEnvironment);
        //    fileClient.UploadFile("1.jpg");
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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

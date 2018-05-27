using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Product.Api;
using CoreFaces.Product.Api.Controllers;
using CoreFaces.Product.Models.Domain;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using CoreFaces.Helper;
using CoreFaces.Identity.Models.Users;

namespace CoreFaces.Product.UnitTest
{
    [TestClass]
    public class LoginControllerTest : BaseTest
    {
      

        [TestMethod]
        public async Task LoginControllerPost()
        {
            CommonApiResponse<UserView> model = await this.Login();
        }

        public async Task<CommonApiResponse<UserView>> Login()
        {
            UserLoginView userLoginView = new UserLoginView { Email = "kuaforx@kuaforx.com", Password = "Huso7474" };
            var response = await _client.PostAsync("/api/login", new StringContent(JsonConvert.SerializeObject(userLoginView), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            CommonApiResponse<UserView> model = JsonConvert.DeserializeObject<CommonApiResponse<UserView>>(responseString);
            return model;
        }

        public async Task<Guid> GetToken()
        {
            CommonApiResponse<UserView> model = await this.Login();
            return model.Result.Jwt.Token;
        }
    }
}

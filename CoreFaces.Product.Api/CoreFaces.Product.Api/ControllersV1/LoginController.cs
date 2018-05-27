using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using CoreFaces.Helper;
using CoreFaces.Product.Models;
using CoreFaces.Identity.Client;
using CoreFaces.Identity.Models.Users;

namespace CoreFaces.Product.Api.ControllersV1
{
    [ApiVersion("1")]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IMemoryCache memoryCache;

        public LoginController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        // POST api/values
        [HttpPost]
        public async Task<CommonApiResponse<UserView>> Post([FromBody]UserLoginView userLoginView)
        {
            Client identityClient = new Client(Config.IdentityServiceBaseUrl);
            UserView userView = await identityClient.LoginAsync(userLoginView);
            if (userView == null)
            {
                return CommonApiResponse<UserView>.Create(Response, System.Net.HttpStatusCode.BadRequest, false, null, "Kullanıcı bilgileri geçersiz.");
            }
            return CommonApiResponse<UserView>.Create(Response, System.Net.HttpStatusCode.OK, true, userView, "");

        }


    }
}

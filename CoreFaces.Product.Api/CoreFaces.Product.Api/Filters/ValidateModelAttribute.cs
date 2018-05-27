using CoreFaces.Helper;
using CoreFaces.Identity.Client;
using CoreFaces.Identity.Models.Domain;
using CoreFaces.Identity.Models.Models.Roles;
using CoreFaces.Identity.Models.Users;
using CoreFaces.Product.Models;
using CoreFaces.Product.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Product.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute, IActionFilter
    {
        
        List<string> requiredRoleList = new List<string>();
        Client identityClient = new Client(Config.IdentityServiceBaseUrl);
        Microsoft.Extensions.Primitives.StringValues _Token = "";

        private readonly RequestDelegate _next;

        public ValidateModelAttribute(RequestDelegate next)
        {
            _next = next;

        }
        public ValidateModelAttribute()
        {

        }
        public ValidateModelAttribute(string _role)
        {
            requiredRoleList = _role.Split(new char[] { ',' }).ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            //if (!context.ModelState.IsValid)
            //{
            //    context.Result = new BadRequestObjectResult(context.ModelState);
            //}
            string requestControllerAndMethodName = context.ActionDescriptor.DisplayName.Replace("Product.Api.Controllers.", "").Replace(" (Product.Api)", ""); ;
            context.HttpContext.Request.Headers.TryGetValue("Token", out _Token);
            bool isAccess = false;
            if (_Token.Count > 0)
            {
                UserView visitorUser = null;
                ProductDatabaseContext _productDatabaseContext = (ProductDatabaseContext)context.HttpContext.RequestServices.GetService(typeof(ProductDatabaseContext));
                //IJwtService _jwtService = (JwtService)context.HttpContext.RequestServices.GetService(typeof(IJwtService));
                UserView systemUserView = AsyncHelpers.RunSync<UserView>(() => identityClient.GetSystemUserCacheAsync(new UserLoginView { Email = Config.IdentitySystemUserName, Password = Config.IdentitySystemPassword }));
                List<RoleView> systemRoleList = AsyncHelpers.RunSync<List<RoleView>>(() => identityClient.GetRoleListCacheAsync(systemUserView.Jwt.Token));
                List<Permission> permissionList = AsyncHelpers.RunSync<List<Permission>>(() => identityClient.GetPermissionListCacheAsync(systemUserView.Jwt.Token));

                Guid token;
                Jwt jwt = new Jwt();
                try
                {
                    //jwt guid format kontrol ediliyor.
                    token = Guid.Parse(_Token.FirstOrDefault());
                    jwt = identityClient.CheckTokenAsync(token).Result;
                    if (jwt == null)
                    {
                        CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.OK, false, null, "Token geçersiz.");
                        BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                        context.Result = badReq;
                        return;
                    }

                    visitorUser = AsyncHelpers.RunSync<UserView>(() => identityClient.GetUserByTokenAsync(jwt.Token));//userGetirildi.
                }
                catch (Exception ex)
                {
                    CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.InternalServerError, false, null, ex.Message);
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                    return;
                }

                try
                {
                    //Burada jwt ile gelen kullanıcının istekte bulunduğu controller içindeki fonksiyona erişimi olup olmadığı sorgulanıyor
                    isAccess = Client.IsAccessRole(systemRoleList, permissionList, visitorUser.Roles, requestControllerAndMethodName);
                    if (!isAccess)
                    {

                        CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.OK, false, null, "Yetkiniz yok.");
                        BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                        context.Result = badReq;
                        return;
                    }

                    var controller = context.Controller as Controller;
                    controller.ViewBag.Jwt = jwt;
                }
                catch (Exception ex)
                {
                    CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.InternalServerError, false, null, ex.Message);
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                    return;
                }
            }
            else
            {
                CommonApiResponse<dynamic> response = CommonApiResponse<dynamic>.Create(context.HttpContext.Response, System.Net.HttpStatusCode.OK, false, null, "Header Token bulunamadı.");
                ObjectResult badReq = new ObjectResult(response);
                context.Result = badReq;
                return;
            }
        }
    }

}

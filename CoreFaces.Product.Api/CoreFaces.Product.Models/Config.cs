using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models
{
    public static class Config
    {
        public static string IdentityServiceBaseUrl { get; set; } = "http://identity.kuaforx.com/";
        //public static string IdentityServiceBaseUrl { get; set; } = "http://localhost:56037/";

        public static string UserCacheKey
        {
            get
            {
                return "User";
            }
        }
        public static int UserCacheSecond { get; set; }

        public static string IdentitySystemUserName { get; set; } = "productservice@product.com";
        public static string IdentitySystemPassword { get; set; } = "Huso7474";

    }
}

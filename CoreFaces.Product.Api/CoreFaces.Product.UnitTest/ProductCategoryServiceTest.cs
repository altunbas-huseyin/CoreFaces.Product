using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreFaces.Product.Api.Controllers;
using CoreFaces.Product.Models;
using CoreFaces.Product.Models.Domain;
using CoreFaces.Product.Models.Models;
using System;
using System.Collections.Generic;
using CoreFaces.Helper;

namespace CoreFaces.Product.UnitTest
{
    [TestClass]
    public class ProductCategoryServiceTest : BaseTest
    {

        [TestMethod]
        public void Save()
        {
            ProductCategoryView productView = new ProductCategoryView();
            productView.Name = "Bilgisayar";
            productView.Description = "Bilgisayar";
            productView.ApiUserId = Guid.NewGuid();
            
            productView.ProductCategoryTranslationList = new List<ProductCategoryTranslation>();
            productView.ProductCategoryTranslationList.Add(new ProductCategoryTranslation { Language = Enums.Language.Turkish, Name = "Bilgisayar", ProductCategoryId = productView.Id, Description = "Bilgisayar" });
            productView.ProductCategoryTranslationList.Add(new ProductCategoryTranslation { Language = Enums.Language.English, Name = "Computer", ProductCategoryId = productView.Id, Description = "Computer" });

            _productCategoryService.Save(productView);
            //Assert.AreNotSame(true, false);
        }


    }
}

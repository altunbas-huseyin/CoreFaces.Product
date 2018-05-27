using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CoreFaces.Product.Api.Controllers;
using CoreFaces.Product.Models;
using CoreFaces.Product.Models.Domain;
using CoreFaces.Product.Models.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using CoreFaces.Helper;

namespace CoreFaces.Product.UnitTest
{
    [TestClass]
    public class ProductServiceTest : BaseTest
    {

        [TestMethod]
        public void SaveProduct()
        {

            ProductView productView = new ProductView();

            CoreFaces.Product.Models.Domain.Product p = new Models.Domain.Product();
            p.Name = "iphone 6";
            p.StockCode = "0012";
            p.Currency = Enums.Currency.TL;
            p.Price = 8;
            productView.Product = p;

            productView.ProductTranslationList = new List<ProductTranslation>();
            ProductTranslation tr = new ProductTranslation { Language = Enums.Language.Turkish, Name = "Elma", Description = "Elma", Content = "Elma", Title = "Elma", };
            ProductTranslation en = new ProductTranslation { Language = Enums.Language.English, Name = "Apple", Description = "Apple", Content = "Apple", Title = "Apple", };
            productView.ProductTranslationList.Add(tr);
            productView.ProductTranslationList.Add(en);


            //Product.Models.Domain.File file = new Models.Domain.File();

            //string filePath = @"C:\Users\haltunbas\Documents\GitHub\ProductV2\Product.Api\Product.Api\wwwroot\img\1.jpg";
            //using (FileStream stream = System.IO.File.OpenRead(filePath))
            //{
            //    byte[] PhotoBytes = new byte[stream.Length];
            //    stream.Read(PhotoBytes, 0, PhotoBytes.Length);

            //    MemoryStream ms = new MemoryStream(PhotoBytes);
            //    Image img = System.Drawing.Image.FromStream(ms);

            //    file.Data = PhotoBytes;
            //    file.Length = file.Data.Length;
            //    file.Name = "ttt.jpg";
            //    file.Type = Enums.FileType.DefaultImage;
            //    file.FileType = ".jpg";
            //    file.Width = img.Width;
            //    file.Height = img.Height;
            //}

            //productView.FileList = new List<Models.Domain.File>();
            //productView.FileList.Add(file);

            Guid insertGuid = _productService.Save(productView);
            bool result = Guid.TryParse(insertGuid.ToString(), out insertGuid);
            Assert.AreNotSame(result, false);
        }


        [TestMethod]
        public void GetProduct()
        {
            ProductView p = new ProductView();
            p = _productService.GetProductViewByProductId(Guid.Parse("73495261-a806-462a-9c03-4739303960a6"), Guid.Parse("00000000-0000-0000-0000-000000000000"));
            //Product.Models.Domain.File file = p.FileList[0];
            //MemoryStream ms = new MemoryStream(file.Data);
            //Image img = System.Drawing.Image.FromStream(ms);
            //img.Save(@"C:\Users\haltunbas\Documents\GitHub\ProductV2\Product.Api\Product.Api\wwwroot\img\2.jpg");
        }

    }
}

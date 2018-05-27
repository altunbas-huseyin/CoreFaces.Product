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

namespace CoreFaces.Product.UnitTest
{
    [TestClass]
    public class SchemaServiceTest : BaseTest
    {

        [TestMethod]
        public void DropTables()
        {
            //bool result = schemaService.DropTables();
            //Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void EnsureCreated()
        {
            //bool result = schemaService.EnsureCreated();
            //Assert.AreEqual(result, true);
        }

    }
}

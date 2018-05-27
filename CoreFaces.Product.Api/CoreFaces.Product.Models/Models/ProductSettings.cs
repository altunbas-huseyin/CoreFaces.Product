using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Models
{
   public class ProductSettings
    {
        public string FileUploadFolderPath { get; set; }
        public string ProductLicenseDomain { get; set; } = "";
        public string ProductLicenseKey { get; set; } = "";
    }
}

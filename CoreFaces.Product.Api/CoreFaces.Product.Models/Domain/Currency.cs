using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Product.Models.Domain
{
    //Para Birimi
    public class Currency : EntityBase
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
    }
}

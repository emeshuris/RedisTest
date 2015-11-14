using System;
using System.Collections.Generic;

namespace Entities
{
    public class CustomerInfo
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }

    public class OrderInfo
    {
        public CustomerInfo CustomerInfo { get; set; }
        public List<ProductInfo> ProductInfos { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class ProductInfo
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
    }
}

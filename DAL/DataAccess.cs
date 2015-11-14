using System.Collections.Generic;

namespace DAL
{
    public static class DataAccess
    {
        public static List<Entities.OrderInfo> GetOrders()
        {
            List<Entities.OrderInfo> orderInfos = new List<Entities.OrderInfo>();

            for (int i = 0; i < 1000; i++)
            {
                Entities.OrderInfo orderInfo = new Entities.OrderInfo
                {
                    OrderId = i,
                    OrderDate = new System.DateTime(),
                    CustomerInfo = new Entities.CustomerInfo
                    {
                        CompanyName = "bingo",
                        ContactName = "bongo"
                    },
                    ProductInfos = new List<Entities.ProductInfo>()
                };

                for (int j = 0; i < 5; i++)
                {
                    orderInfo.ProductInfos.Add(new Entities.ProductInfo
                    {
                        CategoryName = "categoryname",
                        ProductName = "productName"
                    });
                }

                orderInfos.Add(orderInfo);
            }

            return orderInfos;
        }
    }
}

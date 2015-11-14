using RedisConnection;
using System;
using Entities;

namespace RedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (OrderInfo orderInfo in DAL.DataAccess.GetOrders())
                {
                    OrderInfo orderInfoCached = ApplicationCache.Get<OrderInfo>(orderInfo.OrderId.ToString());

                    if (orderInfoCached != null)
                    {
                        Console.WriteLine("Found: {0}", orderInfo.OrderId.ToString());

                        ApplicationCache.Remove(orderInfo.OrderId.ToString());
                        Console.WriteLine("Removed: {0}", orderInfo.OrderId.ToString());
                    }

                    ApplicationCache.Set(orderInfo.OrderId.ToString(), orderInfo, ApplicationCache.CacheExpirationType.Absolute, new TimeSpan(), System.Runtime.Caching.CacheItemPriority.NotRemovable);
                    Console.WriteLine("Added: {0}", orderInfo.OrderId.ToString());

                    //OrderInfo orderInfo = (OrderInfo)reddisCache.GetCacheItem(order.OrderId.ToString()).Value;
                }
            }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}

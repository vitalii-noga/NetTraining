using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using BusinessLogic.Mapping;
using Infrastructure;
using Infrastructure.Entities;

namespace BusinessLogic.Logic
{
    public class AggregatedCalculator
    {
        private readonly NtContext context;
        private readonly ILog log;

        public AggregatedCalculator(NtContext context, ILog log)
        {
            this.context = context;
            this.log = log;
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }

        public double GetOrderTotal(Guid orderId)
        {
            // Retrieve order details
            var details = context.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            if (details.Count == 0)
                return 0;

            // Sum all details
            var sum = details.Sum(x => x.ProductQuantity * x.Product.Price);
            log.Info($"Order Total: {sum}.");
            return sum;
        }

        public List<DtoOrder> GetLastOrders(int numberOfOrders, bool useIncludeMethod)
        {
            if (useIncludeMethod)
                return GetOrdersWithIncludeMethod(numberOfOrders);
            else
                return GetOrdersWithoutIncludeMethod(numberOfOrders);
        }

        private List<DtoOrder> GetOrdersWithIncludeMethod(int numberOfOrders)
        {
            var orders = new List<DtoOrder>();
            foreach (var order in context.Orders.Include("OrderDetails").OrderByDescending(x => x.DateCreated).Take(numberOfOrders).OrderBy(x => x.DateCreated))
            {
                var ord = Mapper.Map<DtoOrder>(order);
                foreach (var detail in order.OrderDetails)
                {
                    ord.Details.Add(Mapper.Map<DtoOrderDetails>(detail));
                }
                orders.Add(ord);
            }
            return orders;
        }

        private List<DtoOrder> GetOrdersWithoutIncludeMethod(int numberOfOrders)
        {
            var orders = new List<DtoOrder>();
            foreach (var order in context.Orders.OrderByDescending(x => x.DateCreated).Take(numberOfOrders).OrderBy(x => x.DateCreated))
            {
                var ord = Mapper.Map<DtoOrder>(order);
                foreach (var detail in order.OrderDetails)
                {
                    ord.Details.Add(Mapper.Map<DtoOrderDetails>(detail));
                }
                orders.Add(ord);
            }
            return orders;
        }
    }
}

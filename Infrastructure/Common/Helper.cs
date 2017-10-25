using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Entities;
using System.Xml.Linq;
using System.Data;
using Common;

namespace Infrastructure
{
    static public class Helper
    {
        static public List<ClientEntity> GenerateClients(ushort numberOfClients)
        {
            var names = "nick,tom,peter,tanya,ira,vitalii,sergiy,olya,oksana,bohdan,oleksandr,taras,oleh,vasyl".Split(',');
            var random = new Random();
            var clients = new List<ClientEntity>();
            foreach (var index in Enumerable.Range(1, numberOfClients))
            {
                var name = names[random.Next(names.Count() - 1)].FirstLetterToUpper();
                clients.Add(new ClientEntity()
                {
                    Id = Guid.NewGuid(),                    
                    Name = name
                });
            }

            return clients;
        }

        static public List<ProductEntity> GenerateProducts(ushort numberOfProducts)
        {
            var names = "apple,car,bus,bike,dog,cat,house,phone,knife,gun,shirt,cake".Split(',');
            var random = new Random();
            var products = new List<ProductEntity>();
            foreach (var index in Enumerable.Range(1, numberOfProducts))
            {
                var name = names[random.Next(names.Count() - 1)].FirstLetterToUpper();
                products.Add(new ProductEntity()
                {
                    Id = Guid.NewGuid(),
                    Code = name.ToLower() + index,
                    Description = $"Description of {name}",
                    Name = name,
                    Price = Math.Round(random.NextDouble() * 100, 2),
                    Quantity = random.Next(100)
                });
            }

            return products;
        }

        static public List<OrderEntity> GenerateOrders(List<ClientEntity> clients, ushort numberOfOrders)
        {
            var orders = new List<OrderEntity>();
            if (clients.Count() == 0)
                return orders;

            var random = new Random();            
            foreach (var index in Enumerable.Range(1, numberOfOrders))
            {
                orders.Add(new OrderEntity()
                {
                    Id = Guid.NewGuid(),
                    ClientId = clients[random.Next(clients.Count() - 1)].Id,
                    DateCreated = DateTime.Now,
                    Status = 0
                });
            }

            return orders;
        }

        static public DataTable GenerateOrderDetailsTable(List<OrderEntity> orders, List<ProductEntity> products, ushort numberOfOrderDetails)
        {
            var details = new DataTable();

            // Add columns - all properties where property type is value type
            foreach (var property in typeof(OrderDetailsEntity).GetProperties())
            {
                if (property.PropertyType.IsValueType)
                {
                    details.Columns.Add(new DataColumn()
                    {
                        DataType = property.PropertyType,
                        ColumnName = property.Name
                    });
                }
            }

            // Add primary key
            details.PrimaryKey = new DataColumn[1] { details.Columns[details.Columns.IndexOf("Id")] };

            // Generate order details for orders
            var orderDetails = GenerateOrderDetails(orders, products, numberOfOrderDetails);
            if (orderDetails.Count() == 0)
                return details;

            foreach (var entity in orderDetails)
            {
                var row = details.NewRow();
                row["Id"] = entity.Id;
                row["OrderId"] = entity.OrderId;
                row["ProductId"] = entity.ProductId;
                row["ProductQuantity"] = entity.ProductQuantity;
                details.Rows.Add(row);                    
            }

            details.AcceptChanges();
            return details;
        }

        static public List<OrderDetailsEntity> GenerateOrderDetails(List<OrderEntity> orders, List<ProductEntity> products, ushort numberOfOrderDetails)
        {
            var details = new List<OrderDetailsEntity>();            
            if (orders.Count() == 0 || products.Count() == 0)
                return details;

            // Generate order details for orders
            var random = new Random();
            foreach (var order in orders)
            {
                foreach (var index in Enumerable.Range(1, numberOfOrderDetails))
                {
                    details.Add(new OrderDetailsEntity
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = products[random.Next(products.Count() - 1)].Id,
                        ProductQuantity = random.Next(100)
                    });
                }
            }
            return details;
        }
    }
}

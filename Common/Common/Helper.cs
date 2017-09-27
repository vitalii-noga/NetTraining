using System;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using System.Xml.Linq;
using System.Data;

namespace Common
{
    static public class Helper
    {
        static public List<Client> GenerateClients(ushort numberOfClients)
        {
            var names = "nick,tom,peter,tanya,ira,vitalii,sergiy,olya,oksana,bohdan,oleksandr,taras,oleh,vasyl".Split(',');
            var random = new Random();
            var clients = new List<Client>();
            foreach (var index in Enumerable.Range(1, numberOfClients))
            {
                var name = names[random.Next(names.Count() - 1)].FirstLetterToUpper();
                clients.Add(new Client()
                {
                    Id = Guid.NewGuid(),                    
                    Name = name
                });
            }

            return clients;
        }

        static public List<Product> GenerateProducts(ushort numberOfProducts)
        {
            var names = "apple,car,bus,bike,dog,cat,house,phone,knife,gun,shirt,cake".Split(',');
            var random = new Random();
            var products = new List<Product>();
            foreach (var index in Enumerable.Range(1, numberOfProducts))
            {
                var name = names[random.Next(names.Count() - 1)].FirstLetterToUpper();
                products.Add(new Product()
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

        static public List<Order> GenerateOrders(List<Client> clients, ushort numberOfOrders)
        {
            var orders = new List<Order>();
            if (clients.Count() == 0)
                return orders;

            var random = new Random();            
            foreach (var index in Enumerable.Range(1, numberOfOrders))
            {
                orders.Add(new Order()
                {
                    Id = Guid.NewGuid(),
                    ClientId = clients[random.Next(clients.Count() - 1)].Id,
                    DateCreated = DateTime.Now,
                    Status = 0
                });
            }

            return orders;
        }

        static public DataTable GenerateOrderDetails(List<Order> orders, List<Product> products, ushort numberOfOrderDetails)
        {
            var details = new DataTable();

            // Add columns - all properties where property type is value type
            foreach (var property in typeof(OrderDetails).GetProperties())
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
            if (orders.Count() == 0 || products.Count() == 0)
                return details;

            var random = new Random();
            foreach (var order in orders)
            {
                foreach (var index in Enumerable.Range(1, numberOfOrderDetails))
                {
                    var row = details.NewRow();
                    row["Id"] = Guid.NewGuid();
                    row["OrderId"] = order.Id;
                    row["ProductId"] = products[random.Next(products.Count() - 1)].Id;
                    row["ProductQuantity"] = random.Next(100);
                    details.Rows.Add(row);                    
                }
            }

            details.AcceptChanges();
            return details;
        }
    }
}

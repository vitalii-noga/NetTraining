using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class NtContextInitializer : DropCreateDatabaseAlways<NtContext>
    {
        public override void InitializeDatabase(NtContext context)
        {
            base.InitializeDatabase(context);

            context.Database.ExecuteSqlCommand(
                "CREATE PROCEDURE [dbo].[ClientDetails] AS " +
                "SELECT C.[Id], " +
                "  C.[Name], " +
                "  ISNULL(SUM(OD.[ProductQuantity] * P.[Price]), 0) AS OrdersTotal " +
                "FROM [dbo].[Clients] C " +
                "  LEFT OUTER JOIN [dbo].[Orders] O ON O.[ClientId] = C.[Id] " +
                "  LEFT OUTER JOIN [dbo].[OrderDetails] OD ON OD.[OrderId] = O.[Id] " +
                "  LEFT OUTER JOIN [dbo].[Products] P ON P.[Id] = OD.[ProductId] " +
                "GROUP BY C.[Id], C.[Name]");
        }

    }
}

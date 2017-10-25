# NetTraining

## Task 1
- Create Git Repository for Training
- Create Solution for training in that repository
- Create Console Application Project in that Solution
- Create class Product with few properties like ‘ID’, ‘Code’, ‘Name’, ‘Price’, etc …
- Implement IComparable interface in your class so that it compares 2 products by Code property.
- Create hardcoded List of products in your console application using object initializer.

## Task 2
- Using NuGet package manager, add log4net to your application
- Configure RollingFileAppender in app.config file, according to documentation examples http://logging.apache.org/log4net/release/config-examples.html
- Log some messages using log4net logger in your console app
- Create cusom attribute [UseForEqualityCheck]
- Create custom IEqualityComparer that will use [UseForEqualityCheck] attribute to determine which fields will be used for equality check
- use new comparer to remove duplicates from your collection

## Task 3
- Install Entity Framework package from NuGet
- Create EF context in your project, enable migrations
- Add your Product entity to context
- Add new column to Product entity and create new migration for that
- Insert some data to DB using this context
- Read and update data in DB using your context

## Task 4
- Create The following Entities in your model: <br />
**Order** <br />
ID <br />
ClientID <br />
DateCreated <br />
Status <br />
**Client** <br />
ID <br />
Name <br />
**OrderDetails** <br />
ID <br />
OrderID <br />
ProductID <br />
ProductQuantity <br />
**Product** Entity should already exist in your model.
- Generate and Insert Some Products into DB using EF
- Generate and Insert 10 Clients into DB using EF
- Generate and insert 1000 Orders into DB:
  - Insert 500 records with normal context, measure total time of the operations.
  - For measurment use Stopwatch class: https://msdn.microsoft.com/en-us/library/system.diagnostics.stopwatch(v=vs.110).aspx
  - Insert 500 records with context where changes tracking disabled, like described here: https://msdn.microsoft.com/ru-ru/data/jj556205, measure total time of the operations
- Generate 10-15 OrderDetails records for each order. Insert them into DB using SqlBulkCopy class: https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlbulkcopy(v=vs.110).aspx

## Task 5
- Rewrite configuration of your EF models so that it uses EntityTypeConfiguration classes for each entity: http://www.entityframeworktutorial.net/code-first/move-configurations-to-seperate-class-in-code-first.aspx
- Add new project for BL classes and DTOs <br />
Recommended structure is the following (assuming that solutions is DotNetTraining) <br />
Project: DotNetTraining.BusinessLogic <br />
Subfolders: <br />
  - Logic <br />
  - Mapping <br />
  - DTO
- Create class for Aggregated Calcuations. <br />
Pass DBContext and logger to its constructor as dependencies.
- Add SimpleInjector package from NuGet <br />
Register your DB context and Logger as services in IoC container. <br />
QuickStart and other documentation can be found here: https://simpleinjector.readthedocs.io/en/latest/quickstart.html 
- Get instance of your logic class from the container.
- For given order ID calculate total cost of that order. <br />
You can use normal context and entities here.
- For given client ID return information about 10 most recent orders and cost of each order. <br />
Processing orders in loop here runs into N+1 problem of EF. <br />
To avoid this problem, use “include” feature of EF to get all data in one query. <br />
Compare time with “include” in query and without it. <br />
- For each client, get total cost of all their orders. <br />
You will have N+1 problem here without include. <br />
Include fixes N+1 but still causes whole OrderDetails table to be loaded into memory. <br />
To avoid this - use SP for calculations in DB. <br />
Example of SP in code first approach you can find here: http://www.dotnetodyssey.com/2015/03/12/calling-stored-procedure-from-entity-framework-6-code-first/ 
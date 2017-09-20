# NetTraining

##Task 1
- Create Git Repository for Training
- Create Solution for training in that repository
- Create Console Application Project in that Solution
- Create class Product with few properties like ‘ID’, ‘Code’, ‘Name’, ‘Price’, etc …
- Implement IComparable interface in your class so that it compares 2 products by Code property.
- Create hardcoded List of products in your console application using object initializer.

##Task 2
- Using NuGet package manager, add log4net to your application
- Configure RollingFileAppender in app.config file, according to documentation examples http://logging.apache.org/log4net/release/config-examples.html
- Log some messages using log4net logger in your console app

- Create cusom attribute [UseForEqualityCheck]
- Create custom IEqualityComparer that will use [UseForEqualityCheck] attribute to determine which fields will be used for equality check
- use new comparer to remove duplicates from your collection
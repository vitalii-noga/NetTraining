using System;
using System.Collections.Generic;
using System.Reflection;
using Common;

namespace Task02
{
    // This class compare all fields and properties marked with UseForEqualityCheck attribute
    class ProductEqualityComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else
            {
                // Get all equality fields and properties and compare their values
                return 
                    CompareItems<FieldInfo>(GetEqualityItems<FieldInfo>(typeof(Product)), x, y) &&
                    CompareItems<PropertyInfo>(GetEqualityItems<PropertyInfo>(typeof(Product)), x, y);
            }
        }

        public int GetHashCode(Product obj)
        {
            // Get hash of all equality fields values and properties values
            return
                (GetHashCodeForEqualityItems<FieldInfo>(GetEqualityItems<FieldInfo>(typeof(Product)), obj) +
                GetHashCodeForEqualityItems<PropertyInfo>(GetEqualityItems<PropertyInfo>(typeof(Product)), obj)).GetHashCode(); 
        }        

        static List<T> GetEqualityItems<T>(Type type) where T : MemberInfo
        {
            // Current function supports only FieldInfo and PropertyInfo types            
            var equalityItems = new List<T>();
            dynamic items;
            if (typeof(T) == typeof(FieldInfo))
                items = type.GetFields();
            else if (typeof(T) == typeof(PropertyInfo))
                items = type.GetProperties();
            else
                return equalityItems;

            foreach (var item in items)
            {
                var attrs = item.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if (attr is UseForEqualityCheck)
                        equalityItems.Add(item);
                }
            }
            return equalityItems;
        }

        static int GetHashCodeForEqualityItems<T>(List<T> items, object obj) where T : MemberInfo
        {
            // Current function supports only FieldInfo and PropertyInfo types
            int hash = 0;
            dynamic info;            
            foreach (var item in items)
            {
                if (item is FieldInfo)
                {
                    info = item as FieldInfo;
                    if (info != null)
                        hash = (info.GetValue(obj).GetHashCode() + hash).GetHashCode();
                }
                else if (item is PropertyInfo)
                {
                    info = item as PropertyInfo;
                    if (info != null)
                        hash = (info.GetValue(obj).GetHashCode() + hash).GetHashCode();
                }
            }
            return hash;
        }

        static bool CompareItems<T>(List<T> items, object x, object y) where T : MemberInfo
        {
            // Current function supports only FieldInfo and PropertyInfo types
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else
            {
                dynamic info;
                foreach (var item in items)
                {
                    if (item is FieldInfo)
                    {
                        info = item as FieldInfo;
                        if (info && info.GetValue(x) != info.GetValue(y))
                            return false;
                    }
                    else if (item is PropertyInfo)
                    {
                        info = item as PropertyInfo;
                        if (info != null && info.GetValue(x) != info.GetValue(y))
                            return false;
                    }
                }
                return true;
            }
        }
    }
}

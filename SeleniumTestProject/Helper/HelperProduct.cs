using SeleniumTestProject.Entity;
using SeleniumTestProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestProject.Helper
{
    class HelperProduct
    {

        public static void addProducts(List<ProductModel> pmList)
        {
            using (ECommerceDBEntities dbEntity = new ECommerceDBEntities())
            {
                var productList = convertToProduct(pmList);
                dbEntity.T_Product.AddRange(productList);
                dbEntity.SaveChanges();
            }
        }

        public static List<ProductModel> getProducts()
        {
            List<ProductModel> productList = new List<ProductModel>();
            using (ECommerceDBEntities dbEntity = new ECommerceDBEntities())
            {
                var products = (from p in dbEntity.T_Product orderby (p.date) descending select p).ToList();
                foreach (var product in products)
                {
                    ProductModel pm = new ProductModel();
                    pm.id = product.id;
                    pm.companyName = product.companyName;
                    pm.name = product.name;
                    pm.price = product.price;
                    pm.date = product.date;

                    productList.Add(pm);
                }
            }

            return productList;
        }
        public static void deleteAllProducts()
        {
            using (ECommerceDBEntities dbEntity = new ECommerceDBEntities())
            {
                dbEntity.Database.ExecuteSqlCommand("TRUNCATE TABLE [T_Product]");
            }

        }

        public static List<T_Product> convertToProduct(List<ProductModel> pmList)
        {
            List<T_Product> productList = new List<T_Product>();
            foreach (ProductModel pm in pmList)
            {
                T_Product newProduct = new T_Product();
                newProduct.id = pm.id;
                newProduct.companyName = pm.companyName;
                newProduct.name = pm.name;
                newProduct.price = pm.price;
                newProduct.date = pm.date;
                newProduct.discount = pm.discount;
                newProduct.keyword = pm.keyword;
                productList.Add(newProduct);
            }

            return productList;
        }

    }

}





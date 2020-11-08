using System;
using System.Collections.Generic;
using System.Text;

namespace BarCodeTester
{
    class ProductRepository
    {
        private static ProductRepository _instance;

        public static ProductRepository Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new ProductRepository();
                return _instance;
            }
        }

        private List<string> productList;

        public ProductRepository()
        {
            productList = new List<string>();
        }

        public void AddProduct(string prod)
        {
            productList.Add(prod);
        }

        public void RemoveProduct(string prod)
        {
            productList.Remove(prod);
        }

        public List<string> GetAllProducts()
        {
            return productList;
        }
    }
}

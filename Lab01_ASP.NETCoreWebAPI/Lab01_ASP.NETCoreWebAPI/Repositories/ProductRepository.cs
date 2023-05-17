using BusinessObjects;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductReposititory
    {
        public void SaveProduct(Product p)
        => ProductDAO.SaveProduct(p);

        public Product GetProductById(int id)
       => ProductDAO.FindProductById(id);

        public void DeleteProduct(Product p)
        => ProductDAO.DeleteProduct(p);

        public void UpdateProduct(Product p)
        => ProductDAO.UpdateProduct(p);

        public List<Category> GetCategories()
        => CategoryDAO.GetCategories();

        public List<Product> GetProducts()
        => ProductDAO.GetProducts();
    }
}

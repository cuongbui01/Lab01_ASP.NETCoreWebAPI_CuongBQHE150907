using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsControllers : ControllerBase
    {
        private IProductReposititory respository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProduct() => respository.GetProducts();

        [HttpPost]
        public IActionResult PostProduct(Product p) {
            respository.SaveProduct(p);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var p = respository.GetProductById(id);
            if (p == null)
                return NotFound();
            respository.DeleteProduct(p);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, Product p)
        {
            var pTmp = respository.GetProductById(id);
            if (p == null)
                NotFound();
            respository.UpdateProduct(pTmp);
            return NoContent();
        }

    }
}

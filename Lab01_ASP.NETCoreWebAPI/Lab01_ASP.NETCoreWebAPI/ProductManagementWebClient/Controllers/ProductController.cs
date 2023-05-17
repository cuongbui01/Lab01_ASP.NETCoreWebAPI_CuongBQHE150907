using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:5075/ProductsControllers";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(listProducts);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product p)
        {
            using (var respone = await client.PostAsJsonAsync(ProductApiUrl, p))
            {
                string apiResponse = await respone.Content.ReadAsStringAsync();
            }
            return Redirect("/Product/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            List<Product> products = await GetProducts();
            Product product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }

        public async Task<IActionResult> Delete(int id)
        {
            List<Product> products = await GetProducts();
            Product product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            String url = ProductApiUrl + "/id?id=" + id;
            await client.DeleteAsync(url);
            return Redirect("/Product/Index");
        }
        private async Task<List<Product>> GetProducts()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<Product>>(strData, options);
        }
    }
}

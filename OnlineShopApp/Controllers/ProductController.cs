using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class ProductController : Controller
    {
        public string Index(int id)
        {
            var product = ProductsRepository.TryGetById(id);
            if (product is not null)
            {
                return $"{product}{product.Description}"; 
            }
            return $"Товар с идентификатором: {id} отсутствует!";
        }
    }
}

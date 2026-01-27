using System.Text;
using Microsoft.AspNetCore.Mvc;
using OnlineShopApp.Models;
using OnlineShopApp.Repositories;

namespace OnlineShopApp.Controllers
{
    public class HomeController : Controller
    {      

        public string Index()
        {
            List<Product> products = ProductsRepository.GetAll();

            var result = new StringBuilder();
            foreach (var product in products)
            {
                result.AppendLine(product.ToString());
            }

            return result.ToString();
        }

    }
}

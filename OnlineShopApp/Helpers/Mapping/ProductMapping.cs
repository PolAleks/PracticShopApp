using OnlineShop.Db.Models;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Helpers.Mapping
{
    public static class ProductMapping
    {
        public static IEnumerable<ProductViewModel> ToViewModels(this IEnumerable<Product> products)
        {
            var productsViewModel = new List<ProductViewModel>();

            foreach (var product in products)
            {
                productsViewModel.Add(product.ToViewModel());
            }

            return productsViewModel;
        }

        public static ProductViewModel ToViewModel(this Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                PhotoPath = product.PhotoPath
            };
        }

        public static Product ToDbModel(this ProductViewModel productViewModel)
        {
            return new Product()
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Cost = productViewModel.Cost,
                Description = productViewModel.Description,
                PhotoPath = productViewModel.PhotoPath
            };
        }

        public static IEnumerable<Product>? ToDbModels(this IEnumerable<ProductViewModel> products)
        {
            return products?.Select(ToDbModel);
        }
    }
}

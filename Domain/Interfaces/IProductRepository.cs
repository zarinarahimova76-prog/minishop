
using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int id);
    Task<Product> GetProduct(int id);
    Task<List<Product>> GetProducts();

    Task<List<Product>> GetProductsByCategory(int categoryId);
    Task<int> GetAmountOfProducts();
    Task<int> GetAmountOfCategories();
    Task<int> GetAmountOfProductsByCategory(int categoryId);
    Task<decimal> GetMinPriceOfProductByCategory(int categoryId);
    Task<decimal> GetMaxPriceOfProductByCategory(int categoryId);
    Task<decimal> GetAveragePriceOfProductByCategory(int categoryId);
    Task<decimal> GetTotalPriceOfProducts();
}

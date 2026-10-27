
using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
    Product GetProduct(int id);
    List<Product> GetProducts();

    List<Product> GetProductsByCategory(int categoryId);
    int GetAmountOfProducts();
    int GetAmountOfCategories();
    int GetAmountOfProductsByCategory(int categoryId);
    decimal GetMinPriceOfProductByCategory(int categoryId);
    decimal GetMaxPriceOfProductByCategory(int categoryId);
    decimal GetAveragePriceOfProductByCategory(int categoryId);
    decimal GetTotalPriceOfProducts();
}

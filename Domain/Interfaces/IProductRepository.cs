using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
    Product GetProduct(int id);
    List<Product> GetProducts();
}
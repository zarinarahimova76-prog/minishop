
using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private Database _database = new Database();

    public async Task AddProduct(Product product)
    {
        using var connection = _database.GetConnection();

        await connection.ExecuteAsync(
            "insert into products (name, price, category_id) values (@Name, @Price, @CategoryId)",
            new
            {
                Name = product.Name ?? "",
                product.Price,
                product.CategoryId
            });
    }

    public async Task UpdateProduct(Product product)
    {
        using var connection = _database.GetConnection();

        await connection.ExecuteAsync(
            "update products set name = @Name, price = @Price, category_id = @CategoryId where id = @Id",
            new
            {
                product.Id,
                Name = product.Name ?? "",
                product.Price,
                product.CategoryId
            });
    }

    public async Task DeleteProduct(int id)
    {
        using var connection = _database.GetConnection();

        await connection.ExecuteAsync(
            "delete from products where id = @Id",
            new { Id = id });
    }

    public async Task<Product> GetProduct(int id)
    {
        using var connection = _database.GetConnection();

        var product = await connection.QueryFirstOrDefaultAsync<Product>(
            "select id, name, price, category_id as CategoryId from products where id = @Id",
            new { Id = id });

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        return product;
    }

    public async Task<List<Product>> GetProducts()
    {
        using var connection = _database.GetConnection();

        var products = await connection.QueryAsync<Product>(
            "select id, name, price, category_id as CategoryId from products order by id");

        return products.ToList();
    }

    public async Task<List<Product>> GetProductsByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        var products = await connection.QueryAsync<Product>(
            "select id, name, price, category_id as CategoryId from products where category_id = @CategoryId order by id",
            new { CategoryId = categoryId });

        return products.ToList();
    }

    public async Task<int> GetAmountOfProducts()
    {
        using var connection = _database.GetConnection();

        return await connection.QuerySingleAsync<int>(
            "select count(*) from products");
    }

    public async Task<int> GetAmountOfCategories()
    {
        using var connection = _database.GetConnection();

        return await connection.QuerySingleAsync<int>(
            "select count(*) from categories");
    }

    public async Task<int> GetAmountOfProductsByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return await connection.QuerySingleAsync<int>(
            "select count(*) from products where category_id = @CategoryId",
            new { CategoryId = categoryId });
    }

    public async Task<decimal> GetMinPriceOfProductByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return await connection.QuerySingleAsync<decimal>(
            "select min(price) from products where category_id = @CategoryId",
            new { CategoryId = categoryId });
    }

    public async Task<decimal> GetMaxPriceOfProductByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return await connection.QuerySingleAsync<decimal>(
            "select max(price) from products where category_id = @CategoryId",
            new { CategoryId = categoryId });
    }

    public async Task<decimal> GetAveragePriceOfProductByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return await connection.QuerySingleAsync<decimal>(
            "select avg(price) from products where category_id = @CategoryId",
            new { CategoryId = categoryId });
    }

    public async Task<decimal> GetTotalPriceOfProducts()
    {
        using var connection = _database.GetConnection();

        return await connection.QuerySingleAsync<decimal>(
            "select coalesce(sum(price), 0) from products");
    }
}

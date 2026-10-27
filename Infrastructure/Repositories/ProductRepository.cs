
using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private Database _database = new Database();

    public void AddProduct(Product product)
    {
        using var connection = _database.GetConnection();

        connection.Execute(
            "insert into products (name, price, category_id) values (@Name, @Price, @CategoryId)",
            new
            {
                Name = product.Name ?? "",
                product.Price,
                product.CategoryId
            });
    }

    public void UpdateProduct(Product product)
    {
        using var connection = _database.GetConnection();

        connection.Execute(
            "update products set name = @Name, price = @Price, category_id = @CategoryId where id = @Id",
            new
            {
                product.Id,
                Name = product.Name ?? "",
                product.Price,
                product.CategoryId
            });
    }

    public void DeleteProduct(int id)
    {
        using var connection = _database.GetConnection();

        connection.Execute(
            "delete from products where id = @Id",
            new { Id = id });
    }

    public Product GetProduct(int id)
    {
        using var connection = _database.GetConnection();

        var product = connection.QueryFirstOrDefault<Product>(
            "select id, name, price, category_id as CategoryId from products where id = @Id",
            new { Id = id });

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        return product;
    }

    public List<Product> GetProducts()
    {
        using var connection = _database.GetConnection();

        return connection.Query<Product>(
            "select id, name, price, category_id as CategoryId from products order by id")
            .ToList();
    }

    public List<Product> GetProductsByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return connection.Query<Product>(
            "select id, name, price, category_id as CategoryId from products where category_id = @CategoryId order by id",
            new { CategoryId = categoryId }).ToList();
    }

    public int GetAmountOfProducts()
    {
        using var connection = _database.GetConnection();

        return connection.QuerySingle<int>(
            "select count(*) from products");
    }

    public int GetAmountOfCategories()
    {
        using var connection = _database.GetConnection();

        return connection.QuerySingle<int>(
            "select count(*) from categories");
    }

    public int GetAmountOfProductsByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return connection.QuerySingle<int>(
            "select count(*) from products where category_id = @CategoryId",
            new { CategoryId = categoryId });
    }

    public decimal GetMinPriceOfProductByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return connection.QuerySingle<decimal>(
            "select min(price) from products where category_id = @CategoryId",
            new { CategoryId = categoryId });
    }

    public decimal GetMaxPriceOfProductByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return connection.QuerySingle<decimal>(
            "select max(price) from products where category_id = @CategoryId",
            new { CategoryId = categoryId });
    }

    public decimal GetAveragePriceOfProductByCategory(int categoryId)
    {
        using var connection = _database.GetConnection();

        return connection.QuerySingle<decimal>(
            "select avg(price) from products where category_id = @CategoryId",
            new { CategoryId = categoryId });
    }

    public decimal GetTotalPriceOfProducts()
    {
        using var connection = _database.GetConnection();

        return connection.QuerySingle<decimal>(
            "select coalesce(sum(price), 0) from products");
    }

}

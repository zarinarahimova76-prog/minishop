
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private Database _database = new Database();

    public void AddProduct(Product product)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand("insert into products (name, price, category_id) values (@name, @price, @category_id)", connection);

        command.Parameters.AddWithValue("@name", product.Name ?? "");
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@category_id", product.CategoryId);
        command.ExecuteNonQuery();
    }

    public void UpdateProduct(Product product)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand( "update products set name = @name, price = @price, category_id = @category_id where id = @id", connection);

        command.Parameters.AddWithValue("@id", product.Id);
        command.Parameters.AddWithValue("@name", product.Name?? "" );
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@category_id", product.CategoryId);

        command.ExecuteNonQuery();
    }

    public void DeleteProduct(int id)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand("delete from products where id = @id", connection);

        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    public Product GetProduct(int id)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand( "select id, name, price, category_id from products where id = @id",  connection);

        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                CategoryId = reader.GetInt32(3)
            };
        }

        throw new Exception("Product not found");
    }

    public List<Product> GetProducts()
    {
        var products = new List<Product>();

        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand( "select id, name, price, category_id from products order by id", connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                CategoryId = reader.GetInt32(3)
            });
        }

        return products;
    }
}

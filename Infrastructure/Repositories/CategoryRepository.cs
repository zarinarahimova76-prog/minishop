using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
   private Database _database = new Database();

    public void AddCategory(Category category)
    {
        using var connection = _database.GetConnection();
        connection.Open();
        using var command = new NpgsqlCommand("insert into categories (name) values (@name)", connection);

        command.Parameters.AddWithValue("@name", category.Name);
        command.ExecuteNonQuery();
    }

    public void UpdateCategory(Category category)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand(
            "update categories set name = @name where id = @id", connection);

        command.Parameters.AddWithValue("@id", category.Id);
        command.Parameters.AddWithValue("@name", category.Name);
        command.ExecuteNonQuery();
    }

    public void DeleteCategory(int id)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand( "delete from categories where id = @id", connection);

        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    public Category GetCategory(int id)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand(
            "select id, name from categories where id = @id", connection);

        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Category
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }

        throw new Exception("Category not found");
    }

    public List<Category> GetCategories()
    {
        var categories = new List<Category>();

        using var connection = _database.GetConnection();
        connection.Open();

        using var command = new NpgsqlCommand(
            "select id, name from categories order by id", connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            categories.Add(new Category
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return categories;
    }
}

using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private Database _database = new Database();

    public void AddCategory(Category category)
    {
        using var connection = _database.GetConnection();

        connection.Execute(
            "insert into categories (name) values (@Name)",
            new { category.Name });
    }

    public void UpdateCategory(Category category)
    {
        using var connection = _database.GetConnection();

        connection.Execute(
            "update categories set name = @Name where id = @Id",
            new { category.Id, category.Name });
    }

    public void DeleteCategory(int id)
    {
        using var connection = _database.GetConnection();

        connection.Execute(
            "delete from categories where id = @Id",
            new { Id = id });
    }

    public Category GetCategory(int id)
    {
        using var connection = _database.GetConnection();

        var category = connection.QueryFirstOrDefault<Category>(
            "select id, name from categories where id = @Id",
            new { Id = id });

        if (category == null)
        {
            throw new Exception("Category not found");
        }

        return category;
    }

    public List<Category> GetCategories()
    {
        using var connection = _database.GetConnection();

        return connection.Query<Category>(
            "select id, name from categories order by id").ToList();
    }
}

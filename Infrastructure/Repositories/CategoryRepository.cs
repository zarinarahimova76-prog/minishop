
using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private Database _database = new Database();

    public async Task AddCategory(Category category)
    {
        using var connection = _database.GetConnection();

        await connection.ExecuteAsync(
            "insert into categories (name) values (@Name)",
            new { category.Name });
    }

    public async Task UpdateCategory(Category category)
    {
        using var connection = _database.GetConnection();

        await connection.ExecuteAsync(
            "update categories set name = @Name where id = @Id",
            new { category.Id, category.Name });
    }

    public async Task DeleteCategory(int id)
    {
        using var connection = _database.GetConnection();

        await connection.ExecuteAsync(
            "delete from categories where id = @Id",
            new { Id = id });
    }

    public async Task<Category> GetCategory(int id)
    {
        using var connection = _database.GetConnection();

        var category = await connection.QueryFirstOrDefaultAsync<Category>(
            "select id, name from categories where id = @Id",
            new { Id = id });

        if (category == null)
        {
            throw new Exception("Category not found");
        }

        return category;
    }

    public async Task<List<Category>> GetCategories()
    {
        using var connection = _database.GetConnection();

        var categories = await connection.QueryAsync<Category>(
            "select id, name from categories order by id");

        return categories.ToList();
    }
}

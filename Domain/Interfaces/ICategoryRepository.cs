using Domain.Models;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    Task AddCategory(Category category);
    Task UpdateCategory(Category category);
    Task DeleteCategory(int id);
    Task<Category> GetCategory(int id);
    Task<List<Category>> GetCategories();
}
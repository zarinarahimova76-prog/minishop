using Domain.Models;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int id);
    Category GetCategory(int id);
    List<Category> GetCategories();
}
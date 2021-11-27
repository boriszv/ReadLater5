using Entity;
using Entity.Search;
using System.Collections.Generic;

namespace Services
{
    public interface ICategoryService
    {
        Category CreateCategory(Category category);
        List<Category> GetCategories(CategorySearch search);
        Category GetCategory(int Id);
        Category GetCategory(string Name);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        List<TDto> GetCategoryDtos<TDto>(CategorySearch search);
        TDto GetCategoryDto<TDto>(int id);
        bool CategoryExists(int categoryid);
    }
}

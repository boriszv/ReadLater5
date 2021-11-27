using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Entity;
using Entity.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        private IMapper _mapper;

        public CategoryService(ReadLaterDataContext readLaterDataContext, IMapper mapper)
        {
            _ReadLaterDataContext = readLaterDataContext;
            _mapper = mapper;
        }

        public Category CreateCategory(Category category)
        {
            _ReadLaterDataContext.Add(category);
            _ReadLaterDataContext.SaveChanges();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _ReadLaterDataContext.Update(category);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Category> GetCategories(CategorySearch search)
        {
            var query = _ReadLaterDataContext.Categories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search.UserID))
            {
                query = query.Where(x => x.UserID == search.UserID);
            }
            
            return query.ToList();
        }

        public Category GetCategory(int Id)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefault();
        }

        public Category GetCategory(string Name)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.Name == Name).FirstOrDefault();
        }

        public void DeleteCategory(Category category)
        {
            _ReadLaterDataContext.Categories.Remove(category);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<TDto> GetCategoryDtos<TDto>(CategorySearch search)
        {
            var query = _ReadLaterDataContext.Categories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search.UserID))
            {
                query = query.Where(x => x.UserID == search.UserID);
            }

            return query
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public TDto GetCategoryDto<TDto>(int id)
        {
            return _ReadLaterDataContext.Categories
                .Where(x => x.ID == id)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public bool CategoryExists(int id)
        {
            return _ReadLaterDataContext.Categories.Any(x => x.ID == id);
        }
    }
}

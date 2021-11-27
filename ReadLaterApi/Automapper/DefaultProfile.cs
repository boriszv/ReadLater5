using AutoMapper;
using Entity;
using ReadLaterApi.Dto;

namespace ReadLaterApiAutomapper
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<Bookmark, BookmarkDto>();
            CreateMap<Category, CategoryDto>();

            CreateMap<BookmarkCreateDto, Bookmark>();
            CreateMap<BookmarkUpdateDto, Bookmark>().ReverseMap();

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }
    }
}

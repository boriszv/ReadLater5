using Entity;
using Entity.Search;
using System.Collections.Generic;

namespace Services
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(Bookmark bookmark);
        List<Bookmark> GetCategories(BookmarkSearch search);
        Bookmark GetBookmark(int Id);
        TDto GetBookmarkDto<TDto>(int Id);
        void UpdateBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
        List<Bookmark> GetBookmarks(BookmarkSearch search);
        List<TDto> GetBookmarkDtos<TDto>(BookmarkSearch search);
    }
}

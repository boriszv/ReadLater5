using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Entity;
using Entity.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        private IMapper _mapper;

        public BookmarkService(ReadLaterDataContext readLaterDataContext, IMapper mapper)
        {
            _ReadLaterDataContext = readLaterDataContext;
            _mapper = mapper;
        }
        public List<Bookmark> GetBookmarks(BookmarkSearch search)
        {
            var query = _ReadLaterDataContext.Bookmark.AsQueryable();

            if (search.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == search.CategoryId);
            }

            return query.ToList();
        }

        public List<TDto> GetBookmarkDtos<TDto>(BookmarkSearch search)
        {
            var query = _ReadLaterDataContext.Bookmark.AsQueryable();

            if (search.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == search.CategoryId);
            }

            return query
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            bookmark.CreateDate = DateTime.UtcNow;
            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Bookmark> GetCategories(BookmarkSearch search)
        {
            var query = _ReadLaterDataContext.Bookmark.AsQueryable();

            return query.ToList();
        }

        public Bookmark GetBookmark(int Id)
        {
            return _ReadLaterDataContext.Bookmark
                .Include(x => x.Category)
                .Where(c => c.ID == Id)
                .FirstOrDefault();
        }

        public TDto GetBookmarkDto<TDto>(int id)
        {
            return _ReadLaterDataContext.Bookmark
                .Where(x => x.ID == id)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}

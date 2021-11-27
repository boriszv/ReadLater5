using AutoMapper;
using Entity;
using Entity.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReadLaterApi.Dto;
using Services;
using System.Collections.Generic;

namespace ReadLaterApi.Controllers
{
    [Authorize]
    [Route("categories/{categoryid}/bookmarks")]
    public class BookmarksApiController : BaseController
    {
        readonly IBookmarkService _bookmarkService;
        readonly ICategoryService _categoryService;
        readonly IMapper _mapper;

        public BookmarksApiController(IBookmarkService bookmarkService, ICategoryService categoryService, IMapper mapper)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(List<BookmarkDto>), StatusCodes.Status200OK)]
        public IActionResult GetList([FromRoute] int categoryid)
        {
            if (!_categoryService.CategoryExists(categoryid))
            {
                return NotFound();
            }

            BookmarkSearch search = new()
            {
                CategoryId = categoryid,
            };

            var result = _bookmarkService.GetBookmarkDtos<BookmarkDto>(search);
            return Ok(result);
        }

        [ProducesResponseType(typeof(BookmarkDto), StatusCodes.Status200OK)]
        [HttpGet("{id}", Name = nameof(GetBookmark))]
        public IActionResult GetBookmark(int id)
        {
            var result = _bookmarkService.GetBookmarkDto<BookmarkDto>(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        
        [ProducesResponseType(typeof(BookmarkDto), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult Add([FromBody] BookmarkCreateDto dto)
        {
            var entity = _mapper.Map<Bookmark>(dto);
            _bookmarkService.CreateBookmark(entity);

            return CreatedAtRoute(nameof(GetBookmark), new { id = entity.ID }, _mapper.Map<BookmarkDto>(entity));
        }
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BookmarkUpdateDto dto)
        {
            var entity = _bookmarkService.GetBookmark(id);
            _mapper.Map(dto, entity);
            _bookmarkService.UpdateBookmark(entity);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] JsonPatchDocument<BookmarkUpdateDto> patchDocument)
        {
            var entity = _bookmarkService.GetBookmark(id);
            var dto = _mapper.Map<BookmarkUpdateDto>(entity);

            patchDocument.ApplyTo(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(dto, entity);

            _bookmarkService.UpdateBookmark(entity);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookmark = _bookmarkService.GetBookmark(id);
            if (bookmark == null)
            {
                return NotFound();
            }
            _bookmarkService.DeleteBookmark(bookmark);

            return NoContent();
        }
    }
}

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
    [Route("categories")]
    public class CategoriesApiController : BaseController
    {
        readonly ICategoryService _categoryService;
        readonly IMapper _mapper;
        
        public CategoriesApiController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        public IActionResult GetList()
        {
            CategorySearch search = new()
            {
                UserID = UserID,
            };

            var result = _categoryService.GetCategoryDtos<CategoryDto>(search);
            return Ok(result);
        }

        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var result = _categoryService.GetCategoryDto<CategoryDto>(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult Add([FromBody] CategoryCreateDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            entity.UserID = UserID;

            _categoryService.CreateCategory(entity);

            return CreatedAtRoute(nameof(GetCategory), new { id = entity.ID }, _mapper.Map<CategoryDto>(entity));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            var entity = _categoryService.GetCategory(id);
            if (entity.UserID != UserID)
            {
                return NotFound();
            }

            _mapper.Map(dto, entity);
            _categoryService.UpdateCategory(entity);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] JsonPatchDocument<CategoryUpdateDto> patchDocument)
        {
            var entity = _categoryService.GetCategory(id);
            var dto = _mapper.Map<CategoryUpdateDto>(entity);

            patchDocument.ApplyTo(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(dto, entity);

            _categoryService.UpdateCategory(entity);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryService.DeleteCategory(category);

            return NoContent();
        }
    }
}

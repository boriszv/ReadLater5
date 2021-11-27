using Entity;
using Entity.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadLater5.ViewModels;
using Services;
using System;
using System.Collections.Generic;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class BookmarksController : BaseController
    {
        IBookmarkService _bookmarkService;
        ICategoryService _categoryService;
        public BookmarksController(IBookmarkService bookmarkService, ICategoryService categoryService)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
        }
        // GET: Bookmarks
        public IActionResult Index([FromQuery] int? categoryId)
        {
            BookmarkSearch search = new()
            {
                CategoryId = categoryId,
            };

            List<Bookmark> model = _bookmarkService.GetBookmarks(search);
            return View(model);
        }

        // GET: Bookmarks/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);

            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // GET: Bookmarks/Create
        public IActionResult Create()
        {
            InitializeCategories();

            return View();
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookmarkCreateViewModel viewModel)
        {
            if (viewModel.CreateCategory && string.IsNullOrWhiteSpace(viewModel.CategoryName))
            {
                ModelState.AddModelError("CategoryName", "Category name cannot be empty");
            }

            if (!ModelState.IsValid)
            {
                InitializeCategories();
                return View(viewModel);
            }

            int? categoryId;
            if (viewModel.CreateCategory)
            {
                Category category = new()
                {
                    Name = viewModel.CategoryName,
                    UserID = UserID,
                };

                _categoryService.CreateCategory(category);
                    
                categoryId = category.ID;
            }
            else
            {
                if (viewModel.CategoryId == -1)
                {
                    viewModel.CategoryId = null;
                }
                categoryId = viewModel.CategoryId;
            }

            _bookmarkService.CreateBookmark(new Bookmark
            {
                URL = viewModel.URL,
                ShortDescription = viewModel.ShortDescription,
                CategoryId = categoryId,
                CreateDate = DateTime.UtcNow,
            });

            return RedirectToAction("Index");
        }

        // GET: Bookmarks/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);

            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }
            return View(bookmark);
        }

        // GET: Bookmarks/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);

            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = _bookmarkService.GetBookmark(id);

            _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }

        private void InitializeCategories()
        {
            var categories = _categoryService.GetCategories(new CategorySearch
            {
                UserID = UserID,
            });

            categories.Insert(0, new Category
            {
                ID = -1,
                Name = "None"
            });

            ViewData["Categories"] = new SelectList(categories, "ID", "Name");
        }
    }
}

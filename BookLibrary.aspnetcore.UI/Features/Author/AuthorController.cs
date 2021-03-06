﻿
namespace BookLibrary.aspnetcore.UI.Features.Author
{
    using AutoMapper;
    using BookLibrary.aspnetcore.Domain;
    using BookLibrary.aspnetcore.Services.Interfaces;
    using BookLibrary.aspnetcore.UI.Features.Book;
    using BookLibrary.aspnetcore.UI.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AuthorController : BaseController
    {
        private IBookService _bookService;
        private IAuthorService _authorService;

        public AuthorController(IBookService bookService,
                               IAuthorService authorService) : base()
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        // GET: Author
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAuthors()
        {
            var authors = await _authorService.GetAll();
            return Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(authors);
        }

        [HttpGet("Author/DetailsPartial/{id}")]
        public async Task<IActionResult> DetailsPartial(Author author)
        {
            return await Task.FromResult(PartialView("_authorDetails", author.MapTo<AuthorViewModel>()));
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View(new AuthorViewModel());
        }

        // POST: Author/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorViewModel authorVM)
        {
            var created = await _authorService.Create(authorVM.MapTo<Author>());
            return created ? Ok() as ActionResult : BadRequest();
        }

        // GET: Author/Edit/5
        [HttpGet("Author/Edit/{id}")]
        public async Task<IActionResult> Edit(Author author)
        {
            return await Task.FromResult(View(author.MapTo<AuthorViewModel>()));
        }

        // POST: Author/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] AuthorViewModel authorVM)
        {
            var edited = await _authorService.Update(authorVM.MapTo<Author>());
            return edited ? Ok() as ActionResult : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorService.Delete(id);
            return deleted ? Ok() as ActionResult : BadRequest();
        }

        public async Task<IEnumerable<BookViewModel>> GetBooks()
        {
            var books = await _bookService.GetAll();
            return Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books);
        }

    }

}

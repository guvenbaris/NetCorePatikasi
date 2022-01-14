using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.BookOperations.CreateBooks;
using BookStoreApp.BookOperations.DeleteBooks;
using BookStoreApp.BookOperations.GetBooks;
using BookStoreApp.BookOperations.UpdateBooks;
using BookStoreApp.DbOperations;
using Microsoft.Extensions.FileProviders;

namespace BookStoreApp.Controllers
{
    [Route("[controller]s")] // controller'ın ismi ve sonuna s takısı ile gelecek.
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //private static List<Book> BookList = new List<Book>
        //{
        //    new Book
        //    {
        //        Id = 1,
        //        GenreId = 1, //Personal Growth
        //        Title = "Lean Startup",
        //        PageCount = 200,
        //        PublishDate = new DateTime(2001,06,12)
        //    },
        //    new Book
        //    {
        //        Id = 2,
        //        GenreId = 2, // Science Finction
        //        Title = "Herland",
        //        PageCount = 250,
        //        PublishDate = new DateTime(2010,06,23)
        //    },
        //    new Book
        //    {
        //        Id = 3,
        //        GenreId = 2, // Science Finction
        //        Title = "Dune",
        //        PageCount = 540,
        //        PublishDate = new DateTime(2001,12,21)
        //    }
        //};

        [HttpGet]
        public IActionResult GetBooks()
        { 
            GetBooksQuery getBooksQuery = new GetBooksQuery(_context,_mapper);
            var result=  getBooksQuery.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] //Root dan almak için
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery getBookByIdQuery = new GetBookByIdQuery(_context,_mapper);

            try
            {
                 var result = getBookByIdQuery.Handle(id);
                 return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpGet] // Query ile id ye göre çekme.
        //public Book GetBookById([FromQuery] string id)
        //{
        //    var book = BookList.Where(b => b.Id ==Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBooksCommand createBooksCommand = new CreateBooksCommand(_context,_mapper);
            
            try
            {
                createBooksCommand.Model = newBook;
                createBooksCommand.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        //Put
        [HttpPut]
        public IActionResult UpdateBook([FromBody] UpdateBookModel updatedBook,[FromQuery] int id)
        {
            UpdateBooksCommand updateBooksCommand = new UpdateBooksCommand(_context);
            try
            {
                updateBooksCommand.Model = updatedBook;
                updateBooksCommand.Handle(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBooksQuery deleteBooksQuery = new DeleteBooksQuery(_context);
            try
            {
                deleteBooksQuery.Handle(id);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}

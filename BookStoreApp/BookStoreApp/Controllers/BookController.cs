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
using FluentValidation;
using FluentValidation.Results;
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
            GetBookQuery getBooksQuery = new GetBookQuery(_context,_mapper);
            var result=  getBooksQuery.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")] //Root dan almak için
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery getBookByIdQuery = new GetBookByIdQuery(_context,_mapper);

            try
            {
                getBookByIdQuery.BookId = id;
                GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
                validator.ValidateAndThrow(getBookByIdQuery);
                var result = getBookByIdQuery.Handle();
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
            CreateBookCommand createBooksCommand = new CreateBookCommand(_context,_mapper);
            
            try
            {
                createBooksCommand.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                //validationResult FluentValidationdan geliyor.
                //ValidationResult result =  validator.Validate(createBooksCommand);
                validator.ValidateAndThrow(createBooksCommand); // Validate and throw et
                createBooksCommand.Handle();
                //if (!result.IsValid)
                //{
                //    foreach (var item in result.Errors)
                //    {
                //        Console.WriteLine("Özellik " + item.PropertyName + " - Error Message" + item.ErrorMessage);
                //        Console.WriteLine("Hata Kodu : " + item.ErrorCode);
                //    }
                //}  

                createBooksCommand.Handle(); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromBody] UpdateBookModel updatedBook,int id)
        {
            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
            try
            {
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                updateBookCommand.Model = updatedBook;
                updateBookCommand.BookId = id;

                validator.ValidateAndThrow(updateBookCommand);
                updateBookCommand.Handle();
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
            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
            try
            {
                deleteBookCommand.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(deleteBookCommand);
                deleteBookCommand.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}

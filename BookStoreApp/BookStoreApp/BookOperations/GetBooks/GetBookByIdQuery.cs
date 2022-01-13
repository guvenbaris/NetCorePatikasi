using System;
using System.Linq;
using BookStoreApp.Common;
using BookStoreApp.DbOperations;

namespace BookStoreApp.BookOperations.GetBooks
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _context;

        public GetBookByIdQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public BookViewModel Handle(int id)
        {
            BookViewModel vm = new BookViewModel(); 
            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book is null)
            {
                throw new InvalidOperationException("Book didn't found.");
            }

            vm.Title = book.Title;
            vm.Genre = ((GenreEnum) book.GenreId).ToString();
            vm.PublishDate = book.PublishDate.Date.ToString("d");
            vm.PageCount = book.PageCount;

            return vm;
        }

    }
}

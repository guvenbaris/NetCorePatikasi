using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApp.Common;
using BookStoreApp.DbOperations;

namespace BookStoreApp.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;

        public GetBooksQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList();

            List<BookViewModel> vm = new List<BookViewModel>();

            foreach (var book in bookList)
            {
                vm.Add(new BookViewModel
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("d"),
                    PageCount = book.PageCount
                });
            }

            return vm;
        }


    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
    }
}

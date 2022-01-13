using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApp.DbOperations;

namespace BookStoreApp.BookOperations.DeleteBooks
{
    public class DeleteBooksQuery
    {
        private readonly BookStoreDbContext _context;

        public DeleteBooksQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book is null)
            {
                throw new InvalidOperationException("Book didn't exist");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}

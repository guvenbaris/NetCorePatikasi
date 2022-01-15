using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApp.DbOperations;

namespace BookStoreApp.BookOperations.DeleteBooks
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public int BookId { get; set; }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Book didn't exist");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}

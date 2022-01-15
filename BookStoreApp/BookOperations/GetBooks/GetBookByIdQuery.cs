using System;
using System.Linq;
using AutoMapper;
using BookStoreApp.Common;
using BookStoreApp.DbOperations;

namespace BookStoreApp.BookOperations.GetBooks
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int BookId { get; set; }

        public GetBookByIdQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookViewModel Handle()
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == BookId);

            if (book is null)
            {
                throw new InvalidOperationException("Book didn't found.");
            }

            BookViewModel vm = _mapper.Map<BookViewModel>(book);
            
            //new BookViewModel();
            //vm.Title = book.Title;
            //vm.Genre = ((GenreEnum) book.GenreId).ToString();
            //vm.PublishDate = book.PublishDate.Date.ToString("d");
            //vm.PageCount = book.PageCount;
            return vm;
        }

    }
}

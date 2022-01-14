﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.Common;
using BookStoreApp.DbOperations;

namespace BookStoreApp.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList();

            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
            
            //new List<BookViewModel>();
            //foreach (var book in bookList)
            //{
            //    vm.Add(new BookViewModel
            //    {
            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreId).ToString(),
            //        PublishDate = book.PublishDate.Date.ToString("d"),
            //        PageCount = book.PageCount
            //    });
            //}

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

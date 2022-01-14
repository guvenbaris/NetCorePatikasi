﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApp.DbOperations;

namespace BookStoreApp.BookOperations.UpdateBooks
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookModel Model { get; set; }
        public int BookId { get; set; }


        public UpdateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Book didn't find.");
            }
            book.Title = Model.Title ?? book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;

            _context.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public int GenreId { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
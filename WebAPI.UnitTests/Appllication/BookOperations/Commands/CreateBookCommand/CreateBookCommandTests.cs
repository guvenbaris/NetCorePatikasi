using System;
using System.Linq;
using AutoMapper;
using BookStoreApp.Application.BookOperations.CreateBooks;
using BookStoreApp.DbOperations;
using BookStoreApp.Entities;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Commands.CreateBookCommand
{
    public class CreateBookCommandTests  : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void InvalidOperationException_WhenAlreadyExistBookTitleIsGiven_ShouldBeReturn()
        {
            //Arrange (Hazırlık)
            var book = new Book()
            {
                Title = "Test_InvalidOperationException_WhenAlreadyExistBookTitleIsGiven_ShouldBeReturn",
                PageCount = 100, PublishDate = new DateTime(1990, 01, 10), GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel(){Title = book.Title};

            //Act & Assert(Çalıştırma)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book has been already exist.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel()
            {
                GenreId = 1,
                PageCount = 1000,
                Title = "Hobbit",
                PublishDate = DateTime.Now.AddYears(-1)
            };

            command.Model = model;
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);

        }
    }
}

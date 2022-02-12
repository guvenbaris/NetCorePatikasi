using System;
using System.Linq;
using BookStoreApp.Application.BookOperations.UpdateBooks;
using BookStoreApp.DbOperations;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Commands.UpdateBookCommands
{
    public class UpdateBookCommandTests : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
        }

        [Fact]
        public void InvalidOperationException_WhenBookIsNotFoundOnDb_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = Int32.MaxValue;
            command.Model = new UpdateBookModel();

            //Act & Assert(Çalıştırma)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book didn't find.");
        }

        [Fact]
        public void WhenValidInput_Book_ShouldBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            var model= new UpdateBookModel
            {
                GenreId = 1,
                Title = "Göçebe"
            };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.FirstOrDefault(book => book.Title == model.Title);

            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}

using System;
using BookStoreApp.Application.BookOperations.DeleteBooks;
using BookStoreApp.DbOperations;
using BookStoreApp.Entities;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Commands.DeleteBookCommands
{
    public class DeleteBookCommandTests  : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;


        public DeleteBookCommandTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
        }

        [Fact]
        public void InvalidOperationException_WhenIfBookNotExistOnDatabase_ShouldBeDontReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = Int32.MaxValue;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Book didn't find");

        }
    }
}

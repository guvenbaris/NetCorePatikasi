using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreApp.Application.GenreOperations.Command.DeleteGenre;
using BookStoreApp.DbOperations;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.GenreOperations.Commands.DeleteGenreCommands
{
    public class DeleteGenreCommandTests : IClassFixture<CommanTextFixture>
    {

        private readonly BookStoreDbContext _context;


        public DeleteGenreCommandTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
        }

        [Fact]
        public void InvalidOperationException_WhenIfGenreNotExistOnDatabase_ShouldBeDontReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = Int32.MaxValue;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Genre has already been deleted.");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreApp.Application.BookOperations.DeleteBooks;
using BookStoreApp.Application.GenreOperations.Command.DeleteGenre;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Commands.DeleteBookCommands
{
   public class DeleteBookCommandValidatorTests : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Int32.MinValue)]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {

            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}

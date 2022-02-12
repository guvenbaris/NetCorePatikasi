using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreApp.Application.GenreOperations.Command.DeleteGenre;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.GenreOperations.Commands.DeleteGenreCommands
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Int32.MinValue)]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnErrors(int id)
        {

            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}

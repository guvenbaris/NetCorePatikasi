using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreApp.Application.GenreOperations.Command.CreateGenre;
using BookStoreApp.Application.GenreOperations.Command.UpdateGenre;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.GenreOperations.Commands.UpdateGenreCommands
{
    public class UpdateGenreCommandValidatorTests :IClassFixture<CommanTextFixture>
    {

        [Theory]
        [InlineData("Act", 0)]
        [InlineData("Act", 1)]
        [InlineData("Acti", Int32.MinValue)]
        [InlineData("Action", -1)]
        [InlineData("", 1)]
        [InlineData("", 0)]

        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(string name,int id)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);

            command.Model = new UpdateGenreModel()
            {
                Name = name,
            };
            command.GenreId = id;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreApp.Application.GenreOperations.Command.CreateGenre;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.GenreOperations.Commands.CreateGenreCommands
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("ab")]
        [InlineData("abc")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {

            CreateGenreCommand command = new CreateGenreCommand(null);

            command.Model = new CreateGenreModel()
            {
                Name = name
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result =  validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}

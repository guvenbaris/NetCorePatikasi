using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreApp.Application.BookOperations.UpdateBooks;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Commands.UpdateBookCommands
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommanTextFixture>
    {
        //Title,GenreId

        [InlineData("Act",0)]
        [InlineData("Act",1)]
        [InlineData("Acti",0)]
        [InlineData("Action",0)]
        [InlineData("",1)]
        [InlineData("",0)]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int genreId)
        {
            //Arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                GenreId = genreId,
                Title = title
            };

            //Act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}

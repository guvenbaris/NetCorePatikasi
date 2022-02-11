using System;
using BookStoreApp.Application.BookOperations.CreateBooks;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Commands.CreateBookCommand
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData("Lord of The Rings",0,0)]
        [InlineData("Lord of The Rings",0,1)]
        [InlineData("Lord of The Rings",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("Lor",100,0)]
        [InlineData("Lor",100,1)]""
        [InlineData("Lord",100,0)]
        [InlineData("Lord",0,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount,int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                PublishDate= DateTime.Now.Date.AddYears(-1),
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 240,
                GenreId = 1,
                PublishDate = DateTime.Now.Date,
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        
    }
}

using System;
using BookStoreApp.Application.BookOperations.GetBooks;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Queries.GetBooksByIdQuery
{
    public class GetBookByIdQueryValidatorTests : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Int32.MinValue)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);

            query.BookId = id;

            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result =  validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}

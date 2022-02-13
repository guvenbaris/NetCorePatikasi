using System;
using BookStoreApp.Application.GenreOperations.Queries.GetGenreDetail;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.GenreOperations.Queries.GetGenresByIdQuery
{
    public class GetGenreByIdQueryValidatorTests : IClassFixture<CommanTextFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(Int32.MinValue)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);

            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}

using System.Collections.Generic;
using AutoMapper;
using BookStoreApp.Application.GenreOperations.Queries.GetGenres;
using BookStoreApp.DbOperations;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.GenreOperations.Queries.GetAllGenreQuery
{
    public class GetGenresQueryTests : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQueryTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void ListGenre_WhenGetAllBook_ShouldBeReturn()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var result = query.Handle();

            result.Count.Should().BeGreaterThan(0);
            result.Should().BeOfType<List<GenreViewModel>>();
        }
    }
}

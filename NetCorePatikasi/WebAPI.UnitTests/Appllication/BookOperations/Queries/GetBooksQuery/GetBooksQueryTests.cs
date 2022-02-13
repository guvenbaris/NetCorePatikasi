using System.Collections.Generic;
using AutoMapper;
using BookStoreApp.Application.BookOperations.GetBooks;
using BookStoreApp.DbOperations;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Queries.GetBooksQuery
{
    public class GetBooksQueryTests : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQueryTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void ListBook_WhenGetAllBook_ShouldBeReturn()
        {

            GetBookQuery query = new GetBookQuery(_context, _mapper);

            var result =  query.Handle();

            result.Count.Should().BeGreaterThan(0);
            result.Should().BeOfType<List<BookViewModel>>();
        }


    }
}

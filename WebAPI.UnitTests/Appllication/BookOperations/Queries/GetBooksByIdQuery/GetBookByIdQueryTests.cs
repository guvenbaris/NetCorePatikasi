using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.Application.BookOperations.GetBooks;
using BookStoreApp.DbOperations;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.BookOperations.Queries.GetBooksByIdQuery
{
    public class GetBookByIdQueryTests : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookByIdQueryTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void InvalidOperationException_WhenIfBookNotExistOnDatabase_ShouldBeReturn()
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = Int32.MaxValue;

            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Book didn't found.");
        }

        [Fact]
        public void Book_WhenGetByIdBook_ShouldBeReturn()
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = 1;

            var result =  query.Handle();

            result.Should().NotBeNull();
            result.Should().BeOfType<BookViewModel>();
        }

    }
}

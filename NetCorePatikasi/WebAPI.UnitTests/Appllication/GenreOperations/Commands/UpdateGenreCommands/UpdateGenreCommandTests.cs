using System;
using System.Linq;
using AutoMapper;
using BookStoreApp.Application.GenreOperations.Command.UpdateGenre;
using BookStoreApp.DbOperations;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.GenreOperations.Commands.UpdateGenreCommands
{
    public class UpdateGenreCommandTests : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }
        [Fact]
        public void InvalidOperationException_WhenGenreIsNotFoundOnDb_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel();
            command.GenreId = Int32.MaxValue;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Genre did not find in DB");
        }

        [Fact]
        public void InvalidOperationException_WhenGenreNameSameToUpdate_ShouldBeReturn()
        {

            var id = 1;

            var genre = _context.Genres.SingleOrDefault(x => x.Id == id);

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel()
            {
                Name = genre.Name,
            };
            command.GenreId = 2;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("There is already a type of book with the same name");
        }

        [Fact]
        public void WhenValidInput_Genre_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Genre",
                IsActive = true,
            };

            command.GenreId = 1;
            command.Model = model;

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(g => g.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name = model.Name;
            genre.IsActive = model.IsActive;
        }

    }
}

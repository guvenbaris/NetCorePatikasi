using System;
using System.Linq;
using BookStoreApp.Application.GenreOperations.Command.CreateGenre;
using BookStoreApp.DbOperations;
using BookStoreApp.Entities;
using FluentAssertions;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Appllication.GenreOperations.Commands.CreateGenreCommands
{
    public class CreateGenreCommandTests : IClassFixture<CommanTextFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommanTextFixture textFixture)
        {
            _context = textFixture.Context;
        }

        [Fact]
        public void InvalidOperationException_WhenAlreadyExistGenreTitleIsGiven_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "InvalidOperationException_WhenAlreadyExistGenreTitleIsGiven_ShouldBeReturn",
                IsActive = true
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel()
            {
                Name = genre.Name
            };

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Genre is already exist");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
        {

            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "TestForCreatedGenre"
            };
            command.Model = model;
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}

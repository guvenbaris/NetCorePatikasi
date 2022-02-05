using BookStoreApp.DbOperations;
using BookStoreApp.Entities;

namespace WebAPI.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                    new Genre {Name = "Poetry Book",},
                    new Genre {Name = "Science Fiction",},
                    new Genre {Name = "Romance",});
        }
    }
}

using BookStoreApp.DbOperations;
using AutoMapper;
using BookStoreApp.Common;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.UnitTests.TestSetup
{
    public class CommanTextFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommanTextFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
            Context = new BookStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>();}).CreateMapper();

        }
    }

}

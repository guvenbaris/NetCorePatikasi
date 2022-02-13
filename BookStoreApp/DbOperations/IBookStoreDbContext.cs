using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.DbOperations
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}

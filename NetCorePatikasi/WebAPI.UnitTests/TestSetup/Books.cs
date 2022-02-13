using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreApp.DbOperations;
using BookStoreApp.Entities;

namespace WebAPI.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book { Title = "Göçebe", GenreId = 1, PageCount = 48, PublishDate = new DateTime(1965, 06, 12) },
                new Book { Title = "İnce Memed", GenreId = 3, PageCount = 436, PublishDate = new DateTime(1955, 05, 24) },
                new Book { Title = "Yaş 35", GenreId = 1, PageCount = 281, PublishDate = new DateTime(1946, 12, 21) });
        }
    }
}


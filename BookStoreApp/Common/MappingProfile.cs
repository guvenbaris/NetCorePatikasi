using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.BookOperations.CreateBooks;
using BookStoreApp.BookOperations.GetBooks;

namespace BookStoreApp.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}

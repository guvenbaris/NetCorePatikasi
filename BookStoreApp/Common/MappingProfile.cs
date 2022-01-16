using AutoMapper;
using BookStoreApp.Application.BookOperations.CreateBooks;
using BookStoreApp.Application.BookOperations.GetBooks;
using BookStoreApp.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreApp.Application.GenreOperations.Queries.GetGenres;
using BookStoreApp.Entities;

namespace BookStoreApp.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src =>src.Genre.Name));
            
            CreateMap<Genre, GetGenresQuery.GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
        }
    }
}

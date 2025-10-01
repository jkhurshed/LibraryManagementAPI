using AutoMapper;
using Library.Dtos.AuthorDtos;
using Library.Dtos.BookDtos;
using Library.Dtos.CategoryDtos;
using Library.Dtos.ReviewDtos;
using Library.Models.Dtos;
using Library.Models.Entities;

namespace Library.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserGetAllDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, UserGetDetailDto>();

        CreateMap<Category, CategoryGetDto>();
        CreateMap<Category, CategoryCreateDto>();
        CreateMap<CategoryCreateDto, Category>();

        CreateMap<Author, AuthorGetDto>();
        CreateMap<Author, AuthorGetDetailDto>();
        CreateMap<AuthorCreateDto, Author>();
        CreateMap<Author, AuthorGetBooksDto>();

        CreateMap<ReviewCreateDto, Review>();
        CreateMap<Review, BooksWithRatingDto>();

        CreateMap<BookCreateDto, Book>();
        CreateMap<Book, BookGetDetailDto>();
    }
}
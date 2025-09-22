using AutoMapper;
using Library.Dtos.AuthorDtos;
using Library.Dtos.CategoryDtos;
using Library.Models.Dtos;
using Library.Models.Entities;

namespace Library.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserGetAllDto>();
        CreateMap<UserGetAllDto, User>();
        CreateMap<User, UserCreateDto>();
        CreateMap<UserCreateDto, User>();

        CreateMap<Category, CategoryGetDto>();
        CreateMap<CategoryGetDto, Category>();
        CreateMap<Category, CategoryCreateDto>();
        CreateMap<CategoryCreateDto, Category>();

        CreateMap<Author, AuthorGetDto>();
        CreateMap<AuthorGetDto, Author>();
        CreateMap<Author, AuthorGetDetailDto>();
        CreateMap<AuthorGetDetailDto, Author>();
        CreateMap<Author, AuthorCreateDto>();
        CreateMap<AuthorCreateDto, Author>();
    }
}
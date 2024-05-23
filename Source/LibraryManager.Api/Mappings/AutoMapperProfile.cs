using AutoMapper;
using LibraryManager.Api.DTOs;
using LibraryManager.Data.Entities;

namespace LibraryManager.Api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<BookDto, Book>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
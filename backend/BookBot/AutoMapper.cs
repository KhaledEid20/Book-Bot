using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookBot.DTOs;

namespace BookBot
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.AuthorName))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.GenreId));
            CreateMap<AddBook, Book>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre));
            CreateMap<Author, AuthorDTO>();
        }
    }
}
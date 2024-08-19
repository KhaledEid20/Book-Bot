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
            CreateMap<Book,BookDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.AuthorName))
                .ForMember(dest => dest.Genre , opt => opt.MapFrom( src => src.GenreId.ToString()));
            

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBot.DTOs
{
    public class BookDto
    {
        public string AuthorName {get; set;}
        public BookGenre Genre {get; set;}
        public string? BookName {get; set;}
        public string? publishDate {get; set;}
        public string Language {get;set;}= string.Empty;
        public decimal Rate{get; set;}
        public byte[]? ImageData { get; set; }
    }
    }
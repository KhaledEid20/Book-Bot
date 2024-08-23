using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BookBot.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [ForeignKey("Author")]
        public int AuthorId {get; set;}
        public Author Author { get; set; }
        public BookGenre GenreId {get; set;}
        public string BookName {get; set;} = string.Empty;
        public string publishDate {get; set;}= string.Empty;
        public string Language {get;set;}= string.Empty;
        public decimal Rate{get; set;}
        public byte[]? ImageData { get; set; }

    }
}
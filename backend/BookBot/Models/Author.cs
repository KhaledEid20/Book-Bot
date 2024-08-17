using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookBot.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = String.Empty;
        public string birthDate{get; set;}= String.Empty;
        public string? deadDate{get; set;}
        public string brief{get; set;}= String.Empty;
    }
}
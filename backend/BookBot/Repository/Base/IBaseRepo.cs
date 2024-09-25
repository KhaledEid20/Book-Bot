using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBot.Repository.Base
{
    public interface IBaseRepo<T> where T :class
    {
        Task<List<Book>> ReturnALlBooks();
        Task<List<Author>> ReturnAllAuthors();
        Task<byte[]> EncodeImage(IFormFile image);
    }
}
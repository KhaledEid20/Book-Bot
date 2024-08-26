using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBot.Repository.Base
{
    public interface IAuthorRepo : IBaseRepo<Author>
    {
        Task<AuthorDTO> AuthorSearch(string name);
        Task<List<BookDto>> AuthorBooks(int id);
        Task<string> AddAuthor(AuthorDTO newAuthor);
        Task<string> DeleteAuthor(int id);
    }
}
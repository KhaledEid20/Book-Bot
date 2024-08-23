using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBot.DTOs;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BookBot.Repository.Base
{
    public interface IBookRepo : IBaseRepo<Book>
    {
        Task<BookDto> GetBookAsync(string name , string? Pd = null);
        Task<BookDto> GetBookDeepSearch(string Name , BookGenre genre , string Lang ,string Pd = null);
        Task<string> addBook(BookDto book);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BookBot.Repository.Base
{
    public interface IBookRepo : IBaseRepo<Book>
    {
        Task getBook(string name , string Pd = null);
    }
}
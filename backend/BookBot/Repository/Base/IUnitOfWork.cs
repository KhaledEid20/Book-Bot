using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBot.Repository.Base
{
    public interface IUnitOfWork  : IDisposable
    {
        IBookRepo book {get;}
        IAuthorRepo author {get;}
    }
}
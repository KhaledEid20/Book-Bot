using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBot.Repository.Base;

namespace BookBot.Repository
{
    public class Unitofwork : IUnitOfWork
    {
        public IBookRepo book { get; private set; }

        public IAuthorRepo author { get; private set; }

        public AppDbContext _context { get; set; }

    public Unitofwork(IBookRepo book, IAuthorRepo author, AppDbContext context)
        {
            this._context = context;
            this.book = book;
            this.author = author;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
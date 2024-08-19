using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBot.Repository.Base;

namespace BookBot.Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        public  AppDbContext _context { get; set; }
        public BaseRepo(AppDbContext context)
        {
            this._context = context;
        }
        
    }
}
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
        public async Task<List<Book>> ReturnALlBooks()
        {
            List<Book> result = null;
            try
            {
                result = await _context.Books.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }
        public async Task<List<Author>> ReturnAllAuthors()
        {
            List<Author> result = null;
            try
            {
                result = await _context.Authors.ToListAsync();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            return result;
        }
    }
}
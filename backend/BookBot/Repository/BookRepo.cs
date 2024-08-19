using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBot.DTOs;

namespace BookBot.Repository
{
    public class BookRepo : BaseRepo<Book>, IBookRepo
    {
        private readonly IMapper _mapper;
        public BookRepo(AppDbContext context , IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        } 
        public async Task<BookDto> GetBookAsync(string name, string? Pd = null)
        {
            string LowerName = name.ToLower();
            Book? result = null;
            if(Pd == null){ // if the user didn't provide a publish date
                try{
                     result = await _context.Books.SingleOrDefaultAsync(nm => nm.BookName.ToLower() == LowerName);
                }
                catch(Exception e){
                    Console.WriteLine(e);
                }
            }
            else{ // if the user provide a publish date
                try
                {
                    result = await (from book in _context.Books
                            where book.BookName.ToLower() == LowerName && book.publishDate == Pd
                            select book).SingleOrDefaultAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            var bookdto = result != null?  _mapper.Map<BookDto>(result) : null;
            return bookdto;
        }


    }
}
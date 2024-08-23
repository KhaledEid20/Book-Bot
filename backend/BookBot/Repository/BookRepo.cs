using System;
using System.Collections.Generic;
using System.Data.Odbc;
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
        public async Task<string> addBook(BookDto book)
        {
            var result = _mapper.Map<Book>(book);
            try{

                _context.Books.Add(result);
                await _context.SaveChangesAsync();
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return "New Book Added";
        }
        public async Task<BookDto> GetBookAsync(string name, string? Pd = null)
        {
            string LowerName = name.ToLower();
            Book? result = null;

            try
            {
                if (Pd == null)
                {
                    result = await _context.Books
                        .Include(b => b.Author)
                        .SingleOrDefaultAsync(nm => nm.BookName.ToLower() == LowerName);
                }
                else
                {
                    result = await _context.Books
                        .Include(b => b.Author)
                        .SingleOrDefaultAsync(book => book.BookName.ToLower() == LowerName && book.publishDate == Pd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Map the Book entity to BookDto
            var bookDto = result != null ? _mapper.Map<BookDto>(result) : null;
            return bookDto;
        }
        public async Task<BookDto> GetBookDeepSearch(string name, BookGenre genre, string lang, string? pd = null)
        {
            Book? result = null;

            try
            {
                if (pd == null)
                {
                    result = await _context.Books
                        .Include(b => b.Author) // Include related Author entity
                        .SingleOrDefaultAsync(book =>
                            book.Language.ToLower() == lang.ToLower() &&
                            book.GenreId == genre &&
                            book.BookName.ToLower() == name.ToLower());
                }
                else
                {
                    result = await _context.Books
                        .Include(b => b.Author) // Include related Author entity
                        .SingleOrDefaultAsync(book =>
                            book.Language.ToLower() == lang.ToLower() &&
                            book.GenreId == genre &&
                            book.BookName.ToLower() == name.ToLower() &&
                            book.publishDate == pd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var final = result != null ? _mapper.Map<BookDto>(result) : null;
            return final;
        }
    }
}
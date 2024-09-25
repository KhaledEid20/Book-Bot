using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using BookBot.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BookBot.Repository
{
    public class BookRepo : BaseRepo<Book>, IBookRepo
    {
        private readonly IMapper _mapper;
        public BookRepo(AppDbContext context , IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }
        public async Task<string> addBook(AddBook book)
        {
            Book result = _mapper.Map<Book>(book);
            result.ImageData = await EncodeImage(book.ImageData);
            try
            {
                _context.Books.Add(result);
                await _context.SaveChangesAsync();

                return $"New Book Added with ID: {result.BookId}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Error: Could not add the book.";
            }
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
            bookDto.ImageData = Convert.ToBase64String(result.ImageData);
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
            final.ImageData = Convert.ToBase64String(result.ImageData);
            return final;
        }
        public async Task<List<string>> searchByGenre(BookGenre genre)
        {
            List<string> result = null;
            try
            {
                 result = await (from i in _context.Books
                                    where i.GenreId == genre
                                    select i.BookName).ToListAsync();
            }
            catch (Exception e) { 
                Console.WriteLine(e);
            }
            return result;
        }
        public async Task<string> DeleteBook(int id)
        {
            var result = await _context.Books.FindAsync(id);
            if(result == null)
            {
                return "Book Not Existed";
            }
            _context.Remove(result);
            _context.SaveChanges();
            return "Book Deleted";
        }
        
    }
}
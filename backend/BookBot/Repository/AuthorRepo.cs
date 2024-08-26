using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookBot.Repository
{
    public class AuthorRepo : BaseRepo<Author> , IAuthorRepo
    {
        public readonly IMapper _mapper;
        public AuthorRepo(AppDbContext context , IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public async Task<string> AddAuthor(AuthorDTO newAuthor)
        {
            Author result = _mapper.Map<Author>(newAuthor); ;
            if (newAuthor == null) return "Enter the Author Info";
            else
            {
                try
                {
                     await _context.Authors.AddAsync(result);
                    await _context.SaveChangesAsync();
                    return "Author has been Added";
                }
                catch (Exception ex) {
                    return ex.ToString();
                }
            }
        }

        public async Task<List<BookDto>> AuthorBooks(int id)
        {
            List<BookDto> dtos = null;
            try
            {
                var result = await (from book in _context.Books.Include(x => x.Author)
                                where book.AuthorId == id
                                select book).ToListAsync();
                dtos = _mapper.Map<List<BookDto>>(result);
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
            return dtos;
        }

        public async Task<AuthorDTO> AuthorSearch(string name)
        {
            Author result = null;
            try
            {
                result = await _context.Authors.FirstOrDefaultAsync(author => author.AuthorName.ToLower() == name.ToLower());
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
            return result != null ? _mapper.Map<AuthorDTO>(result) : null;
        }

        public async Task<string> DeleteAuthor(int id)
        {
            var result = await _context.Authors.FindAsync(id);
            if (result == null) return "Add Author To remove";
            else
            {
                try
                {
                    _context.Authors.Remove(result);
                    await _context.SaveChangesAsync();
                    return "Author has been deleted";
                }
                catch (Exception ex) {
                    return ex.ToString();
                }
            }
        }
    }
}
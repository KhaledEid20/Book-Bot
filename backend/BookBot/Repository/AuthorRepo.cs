using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
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
    }
}
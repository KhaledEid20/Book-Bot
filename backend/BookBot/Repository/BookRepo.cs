using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBot.Repository
{
    public class BookRepo : BaseRepo<Book>, IBookRepo
    {
        private 
        public Task getBook(string name, string Pd = null)
        {
            string x = name.ToLower();
            if(Pd == null){
                
            }
            else{

            }
        }
    }
}
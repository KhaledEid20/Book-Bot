using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBot.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBot
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortenerWithAngular.Models;

namespace UrlShortenerWithAngular.Data
{
    public class UrlDbContext : IdentityDbContext
    {
        
        public UrlDbContext(DbContextOptions options) : base(options)
        {
        }
        
        //DbSet
        public DbSet<Url> Urls { get; set; }

    }

          
    
}

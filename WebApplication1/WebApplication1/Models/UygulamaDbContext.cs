using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class UygulamaDbContext : IdentityDbContext
    {
        //burada entity model arasinda DbContext ile kopru kurdum.
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<emlakturu> emlakturleri { get; set; }
        public DbSet<Emlak> Emlaklar { get; set; }



    }
}

using Microsoft.EntityFrameworkCore;

using WebBanSach.Model;

namespace WebBanSach.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
       public DbSet<Category> Categories { get; set; }
       public DbSet<CoverType> CoverTypes { get; set; }
       public DbSet<Product> Products { get; set; }
        //public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    }
}

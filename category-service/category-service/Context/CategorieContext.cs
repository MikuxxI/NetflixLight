using category_service.Models;
using Microsoft.EntityFrameworkCore;

namespace category_service.Context
{
    public class CategorieContext : DbContext
    {
        public DbSet<Categorie>? Categories { get; set; }

        public CategorieContext(DbContextOptions<CategorieContext> options) : base(options)
        {

        }
    }
}

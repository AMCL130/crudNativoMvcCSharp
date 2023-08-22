using tallerCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace tallerCrud.Data
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext>options): base(options) { 
            
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Producto> Producto { get; set; }
    }
}

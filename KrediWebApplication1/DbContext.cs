using Microsoft.EntityFrameworkCore;

namespace KrediWebApplication1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSet tanımı gerekmez, çünkü direkt Stored Procedure kullanacağız.
    }

}

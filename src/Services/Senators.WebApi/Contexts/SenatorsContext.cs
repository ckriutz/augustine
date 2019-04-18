using Augustine.Senators.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Augustine.Senators.WebApi.Contexts
{
    public class SenatorsContext : DbContext
    {
        public SenatorsContext(DbContextOptions<SenatorsContext> options)
            : base(options)
        { }

        public DbSet<Senator> Senators { get; set; }
    }
}



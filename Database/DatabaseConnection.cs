using Microsoft.EntityFrameworkCore;
using Tebbet.Models;

namespace Tebbet.Database
{
    public class DatabaseConnection : DbContext
    {
        public DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=kandula.db.elephantsql.com;Database=anzexeuv;Username=anzexeuv;Password=F41WE_Mik8cbQJjV3CJINd7HD6B62U8r");
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=anzexeuv;Username=postgres;Password=root");
        }
    }
}

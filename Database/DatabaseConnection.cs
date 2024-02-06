using Microsoft.EntityFrameworkCore;
using Tebbet.Models;

namespace Tebbet.Database
{
    public class DatabaseConnection : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Circuits> Circuits { get; set; }
        public DbSet<Races> Races { get; set; }
        public DbSet<Snails> Snails { get; set; }
        public DbSet<SnailParticipatingRace> SnailParticipatingRace {  get; set; }
        public DbSet<Bets> Bets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=aws-0-eu-west-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.tntbspgkzrdtnncbdzzs;Password=5ENN^aFJ$h5U9f");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=anzexeuv;Username=postgres;Password=root");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public SamuraiContext(DbContextOptions<SamuraiContext> options)
            : base(options)
        { }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnModelCreating
            (ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.BattleId,
                                   s.SamuraiId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Information)
            );
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
    }
}

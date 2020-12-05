using DevelopmentLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseDbContext
{
    public abstract class BaseDatabaseContext : DbContext
    {
        private readonly ILoggerFactory _myDevelopmentLoggerFactory;

        protected BaseDatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected BaseDatabaseContext(DbContextOptions dbContextOptions, string inheritageName) : this(
            dbContextOptions)
        {
            _myDevelopmentLoggerFactory = LoggerFactory.Create(builder => builder
                .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                && level == LogLevel.Information)
                .AddProvider(new CustomDevelopmentLoggerProvider($"{inheritageName}.log")));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myDevelopmentLoggerFactory);
        }
    }
}

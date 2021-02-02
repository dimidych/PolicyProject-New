using System;
using Microsoft.EntityFrameworkCore;

namespace BaseDbContext
{
    public static class BaseDbContextOptions
    {
        public static Action<DbContextOptionsBuilder, string> CreateDbContextOptionsAction = (options, connectionString) =>
            options.UseSqlServer(connectionString,
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
    }
}
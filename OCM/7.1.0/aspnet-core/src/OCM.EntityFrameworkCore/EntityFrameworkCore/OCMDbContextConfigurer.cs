using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace OCM.EntityFrameworkCore
{
    public static class OCMDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<OCMDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<OCMDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

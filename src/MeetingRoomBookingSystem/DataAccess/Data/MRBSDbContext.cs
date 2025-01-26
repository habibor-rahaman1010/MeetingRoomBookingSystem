using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class MRBSDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public MRBSDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }

    }
}

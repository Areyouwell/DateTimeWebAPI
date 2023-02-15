using DTWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DTWebAPI.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Value> Values { get; set; } = null!;
        public DbSet<FileInf> FileInfs { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;
    }
}

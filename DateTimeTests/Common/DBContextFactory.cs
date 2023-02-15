using DTWebAPI.Models;
using DTWebAPI.Services;
using Microsoft.EntityFrameworkCore;


namespace DateTimeTests.Common
{
    public class DBContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            context.Values.AddRange(
                new Value 
                {
                    Id = 58,
                    FullDate = new DateTime(2000, 01, 01),
                    TimeInSec = 36,
                    Index = 869.768,
                    FileId = 8
                },
                new Value
                {
                    Id = 59,
                    FullDate = new DateTime(2000, 02, 01),
                    TimeInSec = 46,
                    Index = 969.768,
                    FileId = 8
                },
                new Value
                {
                    Id = 60,
                    FullDate = new DateTime(2000, 03, 01),
                    TimeInSec = 56,
                    Index = 1069.768,
                    FileId = 9
                },
                new Value
                {
                    Id = 61,
                    FullDate = new DateTime(2000, 04, 01),
                    TimeInSec = 66,
                    Index = 1169.768,
                    FileId = 9
                }
            );

            context.Results.AddRange(
                new Result
                {
                    Id = 5,
                    AllTime = 58,
                    MinDateTime = new DateTime(2008, 05, 04, 7, 0, 0),
                    AverTime = 185,
                    AverIndex = 58.694,
                    MedianIndex = 185.96,
                    MaxIndex = 898.964,
                    MinIndex = 6.94848,
                    CountString = 58,
                    FileId = 9
                },
                new Result
                {
                    Id = 6,
                    AllTime = 59,
                    MinDateTime = new DateTime(2008, 06, 04, 9, 0, 0),
                    AverTime = 186,
                    AverIndex = 106.694,
                    MedianIndex = 186.96,
                    MaxIndex = 896.964,
                    MinIndex = 7.94848,
                    CountString = 59,
                    FileId = 8
                },
                new Result
                {
                    Id = 7,
                    AllTime = 59,
                    MinDateTime = new DateTime(2008, 07, 14, 15, 0, 0),
                    AverTime = 186,
                    AverIndex = 963.694,
                    MedianIndex = 186.96,
                    MaxIndex = 896.964,
                    MinIndex = 7.94848,
                    CountString = 59,
                    FileId = 7
                }
            );

            context.FileInfs.AddRange(
                new FileInf
                {
                    Id = 8,
                    Name = "Rom.csv",
                },
                new FileInf
                {
                    Id = 9,
                    Name = "csvFile.csv",
                },
                new FileInf
                {
                    Id = 7,
                    Name = "csvF.csv",
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

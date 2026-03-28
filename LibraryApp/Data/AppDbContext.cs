using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Seed Data — Database banate waqt automatically data aayega
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Fiction",
                    Description = "Fictional stories and novels"
                },
                new Category
                {
                    Id = 2,
                    Name = "Science",
                    Description = "Science and technology books"
                },
                new Category
                {
                    Id = 3,
                    Name = "History",
                    Description = "Historical books"
                },
                new Category
                {
                    Id = 4,
                    Name = "Programming",
                    Description = "Coding and development books"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id=1,
                    Title="C# in Depth",
                    Author="Jon Skeet",
                    CategoryId=4,
                    Price=599,
                    Pages=500,
                    PublishedDate=new DateTime(2022, 1, 1),
                    Description="Best C# book for developers",
                    IsAvailable=true
                },
                new Book
                {
                    Id=2,
                    Title="Clean Code",
                    Author="Robert Martin",
                    CategoryId=4,
                    Price=499,
                    Pages=431,
                    PublishedDate=new DateTime(2021, 5, 15),
                    Description="Write clean maintainable code",
                    IsAvailable=true
                },
                new Book
                {
                    Id=3,
                    Title="Sapiens",
                    Author="Yuval Noah Harari",
                    CategoryId=3,
                    Price=399,
                    Pages=443,
                    PublishedDate=new DateTime(2020, 3, 10),
                    Description="Brief history of humankind",
                    IsAvailable=false
                }
            );
        }
    }
}

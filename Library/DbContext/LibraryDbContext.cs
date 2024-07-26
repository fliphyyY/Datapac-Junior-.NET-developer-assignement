using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Context
{
    public class LibraryDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<Book>()
                .Property(b => b.Available)
                .HasDefaultValue(true);

            modelBuilder.Entity<BorrowedBook>()
                .HasKey(bb => bb.BorrowId);

            modelBuilder.Entity<BorrowedBook>()
                .HasOne(bb => bb.User)
                .WithMany(u => u.BorrowedBooks)
                .HasForeignKey(bb => bb.UserId);

            modelBuilder.Entity<BorrowedBook>()
                .HasOne(bb => bb.Book)
                .WithOne(b => b.BorrowedBook)
                .HasForeignKey<BorrowedBook>(bb => bb.BookId);

            modelBuilder.Entity<User>().HasData(new IdentityUser<int>
            {
                Id = 1,
                UserName = "user123456",
                NormalizedUserName = "USER123456",
                Email = "test@testing.com",
                NormalizedEmail = "TEST@TESTING.COM",
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString()
            });

        }
    }
}

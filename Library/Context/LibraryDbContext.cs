using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Context
{
    public class LibraryDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }

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

            modelBuilder.Entity<BorrowedBooks>()
                .HasKey(bb => bb.BorrowId);

            modelBuilder.Entity<BorrowedBooks>()
                .HasOne(bb => bb.User)
                .WithMany(u => u.BorrowedBooks)
                .HasForeignKey(bb => bb.UserId);

            modelBuilder.Entity<BorrowedBooks>()
                .HasOne(bb => bb.Book)
                .WithMany(b => b.BorrowedBooks)
                .HasForeignKey(bb => bb.BookId);

           
        }
    }
}

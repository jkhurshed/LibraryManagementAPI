using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Models;

public class LibDbContext(DbContextOptions<LibDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<BookAuthors> BookAuthors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BookAuthors>()
            .HasKey(bc => new {bc.BookId, bc.AuthorId});
        
        modelBuilder.Entity<BookAuthors>()
            .HasOne(bc => bc.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(bc => bc.BookId)
            .OnDelete(DeleteBehavior.Cascade);  
        
        modelBuilder.Entity<BookAuthors>()
            .HasOne(bc => bc.Author)
            .WithMany(c => c.Books)
            .HasForeignKey(bc => bc.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(p => p.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId);
        
        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Book)
            .WithOne(b => b.Inventory)
            .HasForeignKey<Inventory>(i => i.BookId);
        
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);
        
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserId);
        
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);
        
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);
        
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
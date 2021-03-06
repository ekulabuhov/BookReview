using System.Data.Entity;
using System.Data.Entity.Infrastructure; 
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations; 
using BookReview.Migrations;

namespace BookReview.Models
{
    public partial class BooksMvcContext : DbContext
    {
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAspNetUsers> BookAspNetUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Author> Authors { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BooksMvcContext, Configuration>());

            // Relationships
            modelBuilder.Entity<AspNetRole>()
                .HasMany(t => t.AspNetUsers)
                .WithMany(t => t.AspNetRoles)
                .Map(m =>
                {
                    m.ToTable("AspNetUserRoles");
                    m.MapLeftKey("RoleId");
                    m.MapRightKey("UserId");
                });


        }
    }
}

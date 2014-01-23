using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BookReview.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .IsRequired();

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.AspNetUsersId)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Comments");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.PostedOn).HasColumnName("PostedOn");
            this.Property(t => t.BooksId).HasColumnName("BooksId");
            this.Property(t => t.AspNetUsersId).HasColumnName("AspNetUsersId");

            // Relationships
            this.HasRequired(t => t.AspNetUser)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.AspNetUsersId);
            this.HasRequired(t => t.Book)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.BooksId);

        }
    }
}

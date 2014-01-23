using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BookReview.Models.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Books");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Picture).HasColumnName("Picture");
            this.Property(t => t.Publisher).HasColumnName("Publisher");
            this.Property(t => t.ISBN).HasColumnName("ISBN");
            this.Property(t => t.Language).HasColumnName("Language");
            this.Property(t => t.Binding).HasColumnName("Binding");
            this.Property(t => t.Page_extent).HasColumnName("Page_extent");
            this.Property(t => t.Barcode).HasColumnName("Barcode");
            this.Property(t => t.Series).HasColumnName("Series");
            this.Property(t => t.OzonBookId).HasColumnName("OzonBookId");
        }
    }
}

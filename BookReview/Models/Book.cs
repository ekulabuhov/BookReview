using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookReview.Models
{
    public partial class Book
    {
        public Book()
        { 
            this.BookAspNetUsers = new HashSet<BookAspNetUsers>();
            this.Reviews = new HashSet<Review>();

        }

        public int BookId { get; set; }
        public Nullable<int> Year { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string Picture { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string Binding { get; set; }
        public string Page_extent { get; set; }
        public string Barcode { get; set; }
        public string Series { get; set; }
        public Nullable<int> OzonBookId { get; set; } 
        public virtual ICollection<BookAspNetUsers> BookAspNetUsers { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } 

    }

}

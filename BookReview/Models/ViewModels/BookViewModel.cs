using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Models
{
    public class BookViewModel
    {
        public ICollection<Book> Books { get; set; }
        public ICollection<bool> AddedToFavs { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookReview.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Picture { get; set; }
        public int Rating { get; set; } 
    }
}

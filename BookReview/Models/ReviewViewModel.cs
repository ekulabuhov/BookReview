using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Models
{
    public class ReviewViewModel
    {

        public ICollection<Review> Reviews { get; set; } 
        public int BookId { get; set; }
        public string Text { get; set; }
        public System.DateTime PostedOn { get; set; }
        public string FullName { get; set; } 

    }
}
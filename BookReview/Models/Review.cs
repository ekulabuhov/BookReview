using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookReview.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Text { get; set; }
        public System.DateTime PostedOn { get; set; }
        public int UpVotes { get; set; }
        public int BookId { get; set; } 
        public string AspNetUserId { get; set; } 

        public virtual Book Book { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } 
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookReview.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public System.DateTime PostedOn { get; set; }  
        public int ReviewId { get; set; }
        public string AspNetUserId { get; set; }

        public virtual Review Review { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        
    }
}

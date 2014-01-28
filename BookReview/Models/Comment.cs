using System;
using System.Collections.Generic;

namespace BookReview.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public System.DateTime PostedOn { get; set; }
        public int Book_Id { get; set; }
        public string AspNetUser_Id { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Book Book { get; set; }
    }
}

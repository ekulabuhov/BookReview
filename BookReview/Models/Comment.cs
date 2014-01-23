using System;
using System.Collections.Generic;

namespace BookReview.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public System.DateTime PostedOn { get; set; }
        public int BooksId { get; set; }
        public string AspNetUsersId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Book Book { get; set; }
    }
}

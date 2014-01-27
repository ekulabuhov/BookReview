using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookReview.Models
{
    public class BookAspNetUsers
    {
        [Key]
        public int Id { get; set; }
        public string AspNetUserId { get; set; }
        public int BookId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Book Book { get; set; }
    }

}
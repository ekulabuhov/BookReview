using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Models
{
    public class ProfileViewModel
    {
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<Book> FavBooks { get; set; } 
        public ICollection<Review> Reviews { get; set; }
        public bool CanEditProfile { get; set; } 



    }
}
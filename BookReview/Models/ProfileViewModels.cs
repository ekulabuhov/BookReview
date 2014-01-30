using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Models
{
    public class ProfileViewModels
    {
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<Book> FavBooks { get; set; } 
        public ICollection<Review> Review { get; set; }
        public bool CanEditProfile { get; set; } 



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;  
using BookReview.Models;
using Microsoft.AspNet.Identity;

namespace BookReview.Controllers
{
    public class ProfileController : Controller
    {
        private BooksMvcContext db = new BooksMvcContext();

        //
        // GET: /Profile/
        public ActionResult Index(string UserProfile)
        {
            //if no parameter passed, open current user profile
            if (UserProfile == null)
            {
                var queryUserId = User.Identity.GetUserId();
                var result = db.AspNetUsers.Find(queryUserId);
                return View(result);
            }  else
            {
                var result = db.AspNetUsers.First(p => p.FullName == UserProfile);
                return View(result);
            }
        } 

        public ActionResult _FavBooks(string queryProfile)
        {
            var queryUserId = User.Identity.GetUserId();
            var blah = db.AspNetUsers.Find(queryUserId); 
            var result = (from s in db.Books
                          from c in s.BookAspNetUsers
                          where c.AspNetUserId == queryUserId
                          select s).ToList();
            //Check if the current user is the owner of that profile
            if (queryProfile != null && queryProfile != blah.FullName)
            {
               foreach (var huj in result)
                {
                   //this is used as a flag to be passed to view. I decided to go for this approach because I prefer not to use Viewbag..Or because the voices in my head told me to do so...
                    huj.BookAspNetUsers.Clear();
                } 
            }
             
                return PartialView(result);
             
        }



        public ActionResult AddToFavs(int id)
        {
            BookAspNetUsers BookUserJunction = new BookAspNetUsers();
            BookUserJunction.AspNetUserId = User.Identity.GetUserId();
            BookUserJunction.BookId = id;
            db.BookAspNetUsers.Add(BookUserJunction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         

        public ActionResult RemoveFromFav(int id)
        {
            BookAspNetUsers BookUserJunction = db.BookAspNetUsers.Single(b => b.BookId.Equals(id));
            db.BookAspNetUsers.Remove(BookUserJunction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


	}
}
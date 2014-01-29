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
        public ActionResult Index(string commentedBy)
        {
            //if no parameter passed, open current user profile
            if (commentedBy == null)
            {
                var queriedBy = User.Identity.GetUserId();
                var result = db.AspNetUsers.Find(queriedBy);
                return View(result);
            }  else
            {
                var result = db.AspNetUsers.First(p => p.FullName == commentedBy);
                //gotta make a FAKE queries check 
                return View(result);
            }
        }

        public ActionResult _FavBooks(string queriedFavBooks)
        {
            //Залогиненый юзер
            var loggedInUserGUID = User.Identity.GetUserId();
            var loggedInUserDetails = db.AspNetUsers.Find(loggedInUserGUID);
            //Искомый юзер
            var queriedUserDetails = (db.AspNetUsers.Where(b => b.FullName == queriedFavBooks)).Single();
            //var queriedUserGUID = queriedUserDetails.Id;
      
                
            var result = (from s in db.Books
                          from c in s.BookAspNetUsers
                          where c.AspNetUserId == queriedUserDetails.Id
                          select s).ToList(); 
             
                //Check if the current user is the owner of that profile
                if (loggedInUserGUID == null || (queriedFavBooks != null && queriedFavBooks != loggedInUserDetails.FullName))
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
            var loggedInUser = User.Identity.GetUserId();  
            BookAspNetUsers bookUserJunction = (db.BookAspNetUsers.Where(b => b.BookId == id && b.AspNetUserId == loggedInUser)).Single(); 
            db.BookAspNetUsers.Remove(bookUserJunction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


	}
}
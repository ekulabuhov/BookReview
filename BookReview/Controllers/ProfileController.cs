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
        public ActionResult Index()
        {
            var queryUserId = User.Identity.GetUserId();
            var result = db.AspNetUsers.Find(queryUserId);
            return View(result);
        }


        public ActionResult _FavBooks()
        {
            var queryUserId = User.Identity.GetUserId();
            var huj = (from s in db.Books
                      from c in s.BookAspNetUsers
                      where c.AspNetUserId == queryUserId
                      select s).ToList(); 
            return View(huj);
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
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
        public ActionResult Index(string userName)
        {
            var viewModel = new ProfileViewModel();
            AspNetUser queriedUser;

            // Maybe show a picture of a big dick?
            if ((userName == null) && (Request.IsAuthenticated == false))
                return RedirectToAction("Index", "Home");

            string loggedUserId = User.Identity.GetUserId();

            if (userName != null)
                queriedUser = db.AspNetUsers.SingleOrDefault(u => u.FullName == userName);
            else
                queriedUser = db.AspNetUsers.Find(loggedUserId);

            viewModel.ProfilePicture = "http://bardolator23.files.wordpress.com/2011/04/laurence6-44391.jpg";
            viewModel.FullName = queriedUser.FullName;
            viewModel.FavBooks = queriedUser.BookAspNetUsers.Select(x => x.Book).ToList();
            viewModel.CanEditProfile = (queriedUser.Id == loggedUserId);
            return View(viewModel);
        }
         
        public JsonResult AddToFavs(int id)
        {
            BookAspNetUsers BookUserJunction = new BookAspNetUsers();
            BookUserJunction.AspNetUserId = User.Identity.GetUserId();
            BookUserJunction.BookId = id;
            db.BookAspNetUsers.Add(BookUserJunction);
            db.SaveChanges();
            var huj = "<img class='favouriteIcon' src='http://upload.wikimedia.org/wikipedia/commons/3/31/Crystal_Project_Package_favorite.png' />";
            return Json(huj);
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
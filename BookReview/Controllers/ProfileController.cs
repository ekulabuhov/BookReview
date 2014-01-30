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
            var currentLoggedUser = User.Identity.GetUserId();
            var userDetails = db.AspNetUsers.Find(currentLoggedUser);
            ProfileViewModels viewModel = new ProfileViewModels();

            //if no parameter passed, open current user profile
            if ((commentedBy == null) || (commentedBy != null && commentedBy == userDetails.FullName))
            { 
                viewModel.FullName = userDetails.FullName;
                viewModel.ProfilePicture = "http://www.journaldugamer.com/files/2013/04/richardgarriott1.jpg";
                viewModel.FavBooks = (from s in db.Books
                                          from c in s.BookAspNetUsers
                                          where c.AspNetUserId == currentLoggedUser
                                          select s).ToList();
                viewModel.CanEditProfile = true;
                return View(viewModel);
            }   else
            {
                viewModel.ProfilePicture = "http://www.journaldugamer.com/files/2013/04/richardgarriott1.jpg"; 
                var queriedUserDetails = db.AspNetUsers.First(p => p.FullName == commentedBy);
                viewModel.FullName = userDetails.FullName;
                viewModel.FavBooks = (from s in db.Books
                                      from c in s.BookAspNetUsers
                                      where c.AspNetUserId == queriedUserDetails.Id
                                      select s).ToList();
                viewModel.CanEditProfile = false;
                return View(viewModel);
                 
            }
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
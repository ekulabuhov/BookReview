using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BookReview; 
using System.Data;
using System.Data.Entity; 
using System.Net;
using BookReview.Models; 

namespace BookReview.Controllers
{
    public class AddReviewController : Controller
    {
        private BooksMvcContext db = new BooksMvcContext();
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(int bukid, string review)
        {
            Review reviewo = new Review();
            if (ModelState.IsValid)
            {
                reviewo.PostedOn = DateTime.Now;
                reviewo.Text = review;
                reviewo.UpVotes = 0;
                reviewo.BookId = bukid;
                reviewo.AspNetUserId = User.Identity.GetUserId();
                db.Reviews.Add(reviewo);
                db.SaveChanges(); 
            }
            return RedirectToAction("Details", "Book", new { id = bukid });
        }



        // POST: /Reply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int rivjuid, string comment, int bukid)
        { 
            Comment commento = new Comment();
            if (ModelState.IsValid)
            {
                commento.PostedOn = DateTime.Now;  
                commento.Text = comment;
                commento.AspNetUserId = User.Identity.GetUserId();
                commento.ReviewId = rivjuid;
                db.Comments.Add(commento); 
                db.SaveChanges();
                // return RedirectToAction("Details", "Book", new { id = bukid });  @{Html.RenderAction("Reply", "AddComment", new { bukid = Model.Id });} 
             }
            return RedirectToAction("Details", "Book", new { id = bukid }); 
        }

    }
}
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

        public PartialViewResult Reply(int id)
        { 
            var reviewModel = new ReviewViewModel(); 
            reviewModel.Reviews = db.Reviews.Where(x => x.BookId == id).ToList();
            reviewModel.BookId = id;  
            return PartialView(reviewModel); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(int bookId, string reviewText, string name)
        {
            Review reviewo = new Review();
            if (ModelState.IsValid)
            {
                reviewo.PostedOn = DateTime.Now;
                reviewo.Text = reviewText;
                reviewo.UpVotes = 0;
                reviewo.BookId = bookId;
                reviewo.AspNetUserId = User.Identity.GetUserId();
                db.Reviews.Add(reviewo);
                db.SaveChanges();
                
            }
              
            //var reviewModel = new ReviewViewModel();
            //reviewModel.FullName = name;
            //reviewModel.Text = reviewText;
            //reviewModel.PostedOn = DateTime.Now;
            //reviewModel.BookId = bookId;
            //return PartialView("ajaxReview", reviewModel);
            return RedirectToAction("Details", "Book", new { id = bookId });
        }



        // POST: /Reply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult AddComment(int reviewId, string commentText, int bookId, string name)
        { 
            Comment commento = new Comment();
            if (ModelState.IsValid)
            {
                commento.PostedOn = DateTime.Now;
                commento.Text = commentText;
                commento.AspNetUserId = User.Identity.GetUserId();
                commento.ReviewId = reviewId;
                db.Comments.Add(commento); 
                db.SaveChanges(); 
             } 
            var commentModel = new ReviewViewModel();
            commentModel.FullName = name;
            commentModel.Text = commentText;
            commentModel.PostedOn = DateTime.Now;
            commentModel.BookId = bookId;
            return PartialView("ajaxComment", commentModel);
        }


        
    }
}
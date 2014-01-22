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

namespace BookReview.Controllers
{
    public class AddCommentController : Controller
    {

        private BookContext db = new BookContext();
       
        // GET: /Reply/
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Books);
            return View(comments.ToList());
        }

        // GET: /Reply/Create
        public ActionResult Reply()
        { 
            return View();
        }

        // POST: /Reply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(int bukid, string comment)
        { 
            Comments commento = new Comments();
            if (ModelState.IsValid)
            {
                commento.PostedOn = DateTime.Now;
                //gotta use dem migrations to remove that field
                commento.UserId = User.Identity.GetUserId();
                commento.BooksId = bukid;
                commento.Text = comment;
                commento.AspNetUsersId = User.Identity.GetUserId();
                db.Comments.Add(commento); 
                db.SaveChanges();
                // return RedirectToAction("Details", "Book", new { id = bukid });  @{Html.RenderAction("Reply", "AddComment", new { bukid = Model.Id });} 
                 
            }

            return RedirectToAction("Details", "Book", new { id = bukid });
        }

    }
}
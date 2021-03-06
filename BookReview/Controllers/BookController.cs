﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookReview;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Xml;

namespace BookReview.Models
{
    public class BookController : Controller
    {
        private BooksMvcContext db = new BooksMvcContext();

        public string GetFullName()
        {
            string userId = User.Identity.GetUserId();
            return db.AspNetUsers.Single(x => x.Id == userId).FullName;
        }

        

         
        //public ActionResult _FavBooks()
        //{
        //    if (db.Books.Count() < 1)
        //    {
        //        XDocument xdoc = XDocument.Load("C:/Users/ream/Desktop/div_book/books.xml");
        //        List<Book> bookCollection = (from offer in xdoc.Descendants("offer")
        //                                     select new Book
        //                                     {
        //                                         Price = (decimal?)offer.Element("price"),
        //                                         Author = (string)offer.Element("author"),
        //                                         Title = (string)offer.Element("name"),
        //                                         Year = (int?)offer.Element("year"),
        //                                         Description = (string)offer.Element("description"),
        //                                         OzonBookId = (int?)offer.Attribute("id"),
        //                                         Url = (string)offer.Element("url"),
        //                                         Picture = (string)offer.Element("picture"),
        //                                         Publisher = (string)offer.Element("publisher"),
        //                                         ISBN = (string)offer.Element("ISBN"),
        //                                         Language = (string)offer.Element("language"),
        //                                         Binding = (string)offer.Element("binding"),
        //                                         Page_extent = (string)offer.Element("page_extent"),
        //                                         Barcode = (string)offer.Element("barcode"),
        //                                         Series = (string)offer.Element("series"),
        //                                     }).ToList();
        //        db.Books.AddRange(bookCollection);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

    
        public ActionResult Index(string searchString, int? page)
        {
            ViewBag.SearchString = searchString;
            int pageSize = 10;
            int pageIndex = 1; 
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1; 

            string userId = User.Identity.GetUserId();
            IPagedList<BookViewModel> model = (from x in db.Books
                                         where x.Title.Contains(searchString) ||
                                             x.Author.Contains(searchString) ||
                                             x.Description.Contains(searchString)
                                         select new BookViewModel { 
                                             BookId = x.BookId,
                                             Price = x.Price,
                                             Author = x.Author,
                                             Title = x.Title,
                                             Year = x.Year,
                                             Description = x.Description,
                                             OzonBookId = x.OzonBookId,
                                             Url = x.Url,
                                             Picture = x.Picture,
                                             Publisher = x.Publisher,
                                             ISBN = x.ISBN,
                                             Language = x.Language,
                                             Binding = x.Binding,
                                             Page_extent = x.Page_extent,
                                             Barcode = x.Barcode,
                                             Series = x.Series, 
                                             AddedToFavs = false
                                         }).OrderByDescending(m => m.BookId).ToPagedList(pageIndex, pageSize);


            //model = db.Books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString) || b.Description.Contains(searchString)); 
             
            foreach (var item in model)
            {  
                if (item.Description != null)
                {
                    item.Description = item.Description.Substring(0, Math.Min(item.Description.Length, 350));
                    item.Description += "...  ";   
                }
                item.AddedToFavs = db.BookAspNetUsers.Any(rec => rec.BookId == item.BookId && rec.AspNetUserId == userId); 
            }
            
            return View(model);
        }

        // GET: /Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            string userId = User.Identity.GetUserId();
            ViewBag.AddToFavz = db.BookAspNetUsers.Any(rec => rec.BookId == id && rec.AspNetUserId == userId);
            return View(book);
        }

    

        // GET: /Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Year,Title,Description,Price,Author,Url,Picture,Publisher,ISBN,Language,Binding,Page_extent,Barcode,Series,OzonBookId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: /Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Year,Title,Description,Price,Author,Url,Picture,Publisher,ISBN,Language,Binding,Page_extent,Barcode,Series,OzonBookId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: /Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

         
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

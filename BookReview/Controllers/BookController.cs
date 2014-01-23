using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookReview;
using System.Xml.Linq; 

namespace BookReview.Models
{
    public class BookController : Controller
    {
        private BooksMvcContext db = new BooksMvcContext();

        // GET: /Book/
        public ActionResult Index()
        {
            /*
            XDocument xdoc = XDocument.Load("bx.xml");
            List<Books> bookCollection = (from offer in xdoc.Descendants("offer")
                                          select new Books
                                          {
                                              Price = (decimal?)offer.Element("price"),
                                              Author = (string)offer.Element("author"),
                                              Title = (string)offer.Element("name"),
                                              Year = (int?)offer.Element("year"),
                                              Description = (string)offer.Element("description"),
                                              OzonBookId = (int?)offer.Attribute("id"),
                                              Url = (string)offer.Element("url"),
                                              Picture = (string)offer.Element("picture"),
                                              Publisher = (string)offer.Element("publisher"),
                                              ISBN = (string)offer.Element("ISBN"),
                                              Language = (string)offer.Element("language"),
                                              Binding = (string)offer.Element("binding"),
                                              Page_extent = (string)offer.Element("page_extent"),
                                              Barcode = (string)offer.Element("barcode"),
                                              Series = (string)offer.Element("series"),
                                          }).ToList();
            db.Books.AddRange(bookCollection);
            db.SaveChanges();
            */

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult Index(string searchString)
        {
            var searchMainResult = db.Books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString) || b.Description.Contains(searchString));

            foreach (var item in searchMainResult)
            {
                if (item.Description != null)
                {
                    item.Description = item.Description.Substring(0, Math.Min(item.Description.Length, 350));
                    item.Description += "...  ";
                }
            }
            return View(searchMainResult);
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

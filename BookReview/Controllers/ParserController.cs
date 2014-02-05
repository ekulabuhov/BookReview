using BookReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BookReview.Controllers
{
    public class ParserController : Controller
    {

        private BooksMvcContext db = new BooksMvcContext();
        //
        // GET: /Parser/
        public ActionResult Parse()
        {
            Book tempBook = new Book();
            decimal? TPrice = null;
            string TAuthor = null;
            string TTitle = null;
            int? TYear = null;
            string TDescription = null;
            int? TOzonBookId = null;
            string TUrl = null;
            string TPicture = null;
            string TPublisher = null;
            string TISBN = null;
            string TLanguage = null;
            string TBinding = null;
            string TPage_extent = null;
            string TBarcode = null;
            string TSeries = null;

            XmlTextReader reader = new XmlTextReader("C:/Users/ream/Desktop/div_book/books.xml");
            while (reader.Read())
            {
                //если это закрывающий оффер тэг, то хуячим это в базу. Делается это как ты заметил по одной записи. 
                //Можно запихивать кучками, если тебе хочется. Но как по мне то и так довольно быстро.
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "offer")
                {

                    tempBook.Price = TPrice;
                    tempBook.Author = TAuthor;
                    tempBook.Title = TTitle;
                    tempBook.Year = TYear;
                    tempBook.Description = TDescription;
                    tempBook.OzonBookId = TOzonBookId;
                    tempBook.Url = TUrl;
                    tempBook.Picture = TPicture;
                    tempBook.Publisher = TPublisher;
                    tempBook.ISBN = TISBN;
                    tempBook.Language = TLanguage;
                    tempBook.Binding = TBinding;
                    tempBook.Page_extent = TPage_extent;
                    tempBook.Barcode = TBarcode;
                    tempBook.Series = TSeries;


                    db.Books.Add(tempBook);
                    db.SaveChanges();
                }
                else if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name.ToLower())
                    {
                        case "offer":
                            TOzonBookId = Convert.ToInt32(reader.GetAttribute("id"));
                            break;
                        case "price":
                            TPrice = reader.ReadElementContentAsDecimal();
                            break;
                        case "author":
                            TAuthor = reader.ReadElementContentAsString();
                            break;
                        case "name":
                            TTitle = reader.ReadElementContentAsString();
                            break;
                        case "year":
                            TYear = reader.ReadElementContentAsInt();
                            break;
                        case "description":
                            TDescription = reader.ReadElementContentAsString();
                            break;
                        case "url":
                            TUrl = reader.ReadElementContentAsString();
                            break;
                        case "picture":
                            TPicture = reader.ReadElementContentAsString();
                            break;
                        case "publisher":
                            TPublisher = reader.ReadElementContentAsString();
                            break;
                        case "isbn":
                            TISBN = reader.ReadElementContentAsString();
                            break;
                        case "language":
                            TLanguage = reader.ReadElementContentAsString();
                            break;
                        case "binding":
                            TBinding = reader.ReadElementContentAsString();
                            break;
                        case "page_extent":
                            TPage_extent = reader.ReadElementContentAsString();
                            break;
                        case "barcode":
                            TBarcode = reader.ReadElementContentAsString();
                            break;
                        case "series":
                            TSeries = reader.ReadElementContentAsString();
                            break;
                    }
                }
            }


            return View();
        } 
          
       
	}
}
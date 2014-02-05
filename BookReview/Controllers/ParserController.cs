using System.Xml.Linq;
using BookReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BookReview.Controllers
{
    class Category
    {
         
    }

    public class ParserController : Controller
    {
        //public void ParseCategories()
        //{
        //    XmlReader doc = new XmlTextReader(@"C:\Users\eugene.kulabuhov\Downloads\div_book.xml");
        //    IEnumerable<ChemieComponent> result = from c in doc.Descendants("Component")
        //                                          select new ChemieComponent()
        //                                          {
        //                                              Name = (string)c.Attribute("name"),
        //                                              Id = (string)c.Attribute("id"),
        //                                              MolarMass = (double)c.Attribute("molarmass")
        //                                          };
        //}

        private BooksMvcContext db = new BooksMvcContext();
        //
        // GET: /Parser/
        public ActionResult Parse()
        {
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
            int skipCounter = 0;
            int bookCounter = 0;

            db = new BooksMvcContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            db.Configuration.ValidateOnSaveEnabled = false;

            XmlTextReader reader = new XmlTextReader(@"C:\Users\eugene.kulabuhov\Downloads\div_book.xml");
            while (reader.Read())
            {
                //если это закрывающий оффер тэг, то хуячим это в базу. Делается это как ты заметил по одной записи. 
                //Можно запихивать кучками, если тебе хочется. Но как по мне то и так довольно быстро.
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "offer")
                {
                    var tempBook = new Book
                    {
                        Price = TPrice,
                        Author = TAuthor,
                        Title = TTitle,
                        Year = TYear,
                        Description = TDescription,
                        OzonBookId = TOzonBookId,
                        Url = TUrl,
                        Picture = TPicture,
                        Publisher = TPublisher,
                        ISBN = TISBN,
                        Language = TLanguage,
                        Binding = TBinding,
                        Page_extent = TPage_extent,
                        Barcode = TBarcode,
                        Series = TSeries
                    };

                    skipCounter++;

                    if (skipCounter > 114067)
                    {
                        if (bookCounter == 4000)
                        {
                            db.SaveChanges();
                            db = new BooksMvcContext();
                            db.Configuration.AutoDetectChangesEnabled = false;
                            db.Configuration.ValidateOnSaveEnabled = false;
                            bookCounter = 0;
                        }

                        db.Books.Add(tempBook);
                        bookCounter++;
                    }
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

            db.SaveChanges();
            return View();
        } 
          
       
	}
}
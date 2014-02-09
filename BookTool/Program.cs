using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using BookReview.Models;
using System.IO;

namespace BookTool
{
    class Program
    {
        static bool PreRequestHandler(HttpWebRequest request)
        {
            var cookie1 = new Cookie("iwatchyou", "0cab30726ce3d705d55f984923661c08", "", "livelib.ru");
            var cookie2 = new Cookie("LiveLibId", "q6epg7lvu7i7upiftitgscfru7", "", "livelib.ru");
            request.CookieContainer.Add(cookie1);
            request.CookieContainer.Add(cookie2);

            return true;
        }

        static void Main(string[] args)
        {
            var db = new BooksMvcContext();
            int readerCount = 0;
            string author = "";
            string href = "";
            string picture = "";
            string localFilename = "";
            var aftars = new List<Author>();

            HtmlWeb htmlWeb = new HtmlWeb();
            htmlWeb.UseCookies = true;
            htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";

            htmlWeb.PreRequest = new HtmlWeb.PreRequestHandler(PreRequestHandler);

            //htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.76 Safari/537.36";
            //var proxy = new System.Net.WebProxy("187.61.117.11", 8080);
            //var htmlDocument = htmlWeb.Load("http://www.livelib.ru/authors", "GET", proxy, null);

            for (int outer = 2; outer < 40; outer++)
            {
                string nextPage = "http://www.livelib.ru/authors/pop/" + "~" + outer;
                var htmlDocument = htmlWeb.Load(nextPage);
                var navigator = htmlDocument.CreateNavigator();
                aftars.Clear();

                for (int i = 2; i < 27; i++)
                {
                    readerCount =
                       navigator.SelectSingleNode(
                           String.Format("/body[1]/div[4]/div[1]/div[1]/div[1]/table[1]/tr[{0}]/td[2]",
                               i)).ValueAsInt;
                    author =
                        navigator.SelectSingleNode(
                            String.Format("/body[1]/div[4]/div[1]/div[1]/div[1]/table[1]/tr[{0}]/td[1]/a[1]/@title[1]", i))

                            .Value;
                    picture =
                        navigator.SelectSingleNode(
                            String.Format("/body[1]/div[4]/div[1]/div[1]/div[1]/table[1]/tr[{0}]/td[1]/a[1]/img[1]/@src[1]", i))
                            .Value;

                    href =
                        navigator.SelectSingleNode(
                            String.Format("/body[1]/div[4]/div[1]/div[1]/div[1]/table[1]/tr[{0}]/td[1]/a[1]/@href[1]", i))
                            .Value;
                    Debug.WriteLine("{0} - {1} - {2}", author, picture, readerCount, href);
                    //Console.ReadLine();
                    int lastIndex = picture.LastIndexOf("/") + 1;
                    string fileName = picture.Substring(lastIndex);
                    aftars.Add(new Author
                    {
                        Name = author,
                        Picture = fileName,
                        Url = href,
                        Rating = readerCount
                    });


                    if (fileName != "m.gif")
                    {
                        localFilename = @"c:\AuthorPictures\" + fileName;
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(picture, localFilename);
                        }
                    }
                } // end of inner loop

                db.Authors.AddRange(aftars);
                db.SaveChanges();
            }
        }
    }
}
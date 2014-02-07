using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BookTool
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            var htmlDocument = htmlWeb.Load("http://www.livelib.ru/authors");
            var navigator = htmlDocument.CreateNavigator();

            for (int i = 2; i < 20; i++)
            {
                int readerCount =
                    navigator.SelectSingleNode(
                        String.Format("/body[1]/div[4]/div[1]/div[1]/div[1]/table[1]/tr[{0}]/td[2]",
                            i)).ValueAsInt;
                string author =
                    navigator.SelectSingleNode(
                        String.Format("/body[1]/div[4]/div[1]/div[1]/div[1]/table[1]/tr[{0}]/td[1]/a[1]/@title[1]", i))
                        .Value;
                string href =
                    navigator.SelectSingleNode(
                        String.Format("/body[1]/div[4]/div[1]/div[1]/div[1]/table[1]/tr[{0}]/td[1]/a[1]/@href[1]", i))
                        .Value;
                Debug.WriteLine("{0} - {1} - {2}", author, readerCount, href);
            }
        }
    }
}

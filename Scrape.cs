using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using HtmlAgilityPack;

namespace ScrapingTitles
{
    public class Scrape
    {
        public static List<Row> Titles()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://animedao.to/");

            var animeTitles = new List<Row>();

            var Titles = doc.DocumentNode.SelectNodes("//div[@class='latestanime-title']");

            foreach (var t in Titles)
            {
                foreach (var ft in File.ReadAllLines(@".\fixedTitles.txt"))
                {
                    if (t.InnerText.ToLower().Contains(ft))
                    {
                        animeTitles.Add(new Row { Title = t.InnerText });
                    }
                }
            }

            using (var writer = new StreamWriter(@".\Titles.txt"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(animeTitles);
                }
            }

            return animeTitles;
        }
    }
}
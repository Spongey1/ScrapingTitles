using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;
using System;

namespace ScrapingTitles
{
    public class Scrape
    {
        public static void GetTitles()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://myanimelist.net/character.php"); // gets website

            var characterList = new List<Card>();

            var animeNames = doc.DocumentNode.SelectNodes("//*[@class='fs14 fw-b']"); // scrapes via the xPath

            for (int i = 0; i < animeNames.Count; i++)
            {
                string fname, lname, fullName;

                fname = Regex.Replace(animeNames[i].InnerText, @"^([^\s]+)\s+", ""); // removes last word
                lname = Regex.Replace(animeNames[i].InnerText, "\\w+$", ""); // removes first word

                fullName = fname + " " + lname;

                characterList.Add(new Card(i, fullName.Replace(",", "")));

                //GetImages($"https://www.google.com/search?q=site%3A+{name}");
            }

            using (var writer = new StreamWriter(@".\Titles.txt"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(animeNames);
                }
            }
        }

        public static void GetImages(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(url), @"C:\Users\drit0046\VSCode\ScrapingTitles\characterPics");
            }
        }
    }
}
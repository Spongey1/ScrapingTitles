using System;

namespace ScrapingTitles
{
    class Program
    {
        static void Main(string[] args)
        {
            Scrape.Titles().ForEach(title => Console.WriteLine(title));
        }
    }
}

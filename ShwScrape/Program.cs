using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShwScrape
{
    class Program
    {
        static string searchBaseUrl = "https://www.google.de/search?";
        static string scrapesFolder = Environment.CurrentDirectory + @"\Scrapes";

        static async Task<string> Scrape(string scrapeUrl)
        {
            string scrapeResult = await new WebClient().DownloadStringTaskAsync(new Uri(scrapeUrl));

            return scrapeResult;
        }

        static string PhraseCleanse(string uncleanPhrase)
        {
            var result = uncleanPhrase.Replace('+', '_');
            return result.Replace("\"", "");
        }

        static async Task<int> ScrapeOperations(string[] queryPhrases)
        {

            int numScrapes = 0;
            foreach (string phrase in queryPhrases)
            {
                var bounty = await Scrape(searchBaseUrl + $"q={phrase}");

                File.WriteAllText(scrapesFolder + $@"\{PhraseCleanse(phrase)}.html", bounty);
                numScrapes++;

                Console.WriteLine($"{numScrapes}: {phrase}");

            }
            return numScrapes;
        }

        static void PrepareScraper()
        {
            if (!Directory.Exists(scrapesFolder))
            {
                Directory.CreateDirectory(scrapesFolder);
            }
        }

        static async Task Main(string[] args)
        {
            string[] queryPhrases = new string[] { "wirecard+aktie", "apple+aktie", "\"telecom italia\"+aktie" };

            Console.WriteLine("Scraping...");

            PrepareScraper();

            int numScrapes = await ScrapeOperations(queryPhrases);

            Console.WriteLine($"{numScrapes} result files written");
            Console.WriteLine("Press enter to exit: ");
            Console.ReadLine();
        }


    }
}

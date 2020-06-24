using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShwScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            GoGrab("http://www.albahari.com/threading");
        }

        private static void GoGrab(string scrapeeUrl)
        {
            Task<string> task = new WebClient().DownloadStringTaskAsync(new Uri(scrapeeUrl));
            task.ContinueWith(_ =>
            {
                if (task.Exception != null)
                    Console.WriteLine(task.Exception.InnerException.Message);
                else
                {
                    string html = task.Result;
                    Console.Write(html);
                }
            });
        }
    }
}

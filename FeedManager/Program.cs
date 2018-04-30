using System;

namespace FeedManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Scrapper scrapper = new Scrapper();
            string dummy = scrapper.scrapper();


            Console.WriteLine("Hello World!");
        }
    }
}

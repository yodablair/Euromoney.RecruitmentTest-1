using System;
using System.Linq;

namespace ContentConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string content =
                "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
            string[] badWords = new string[] { "swine", "bad", "nasty", "horrible" };

            Console.WriteLine("Scanned the text:");
            Console.WriteLine(content);
            BannedWords bannedWords = new BannedWords(badWords);
            bannedWords.Content = content;

            Console.WriteLine("Total Number of negative words: " + bannedWords.SumOfBannedWords());

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }

    }

}

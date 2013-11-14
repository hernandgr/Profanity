using Profanity.Logic;
using System;
using System.Collections.Generic;

namespace Profanity.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            EvaluateText("I am the good text");
            EvaluateText("Danger!, I am the bad text");

            Console.ReadKey();
        }

        /// <summary>
        /// Evaluates the text.
        /// </summary>
        /// <param name="text">The text.</param>
        private static void EvaluateText(string text)
        {
            ProfanityFilter profanityFilter = new ProfanityFilter(GetBlackList());

            bool textContainsProfanity = profanityFilter.ValidateTextContainsProfanity(text);
            string cleanText = profanityFilter.CleanTextProfanity(text);

            Console.WriteLine("Text to evaluate: " + text);
            Console.WriteLine("Text contains profanity?: " + textContainsProfanity.ToString());
            Console.WriteLine("Clean text: " + cleanText);
            Console.WriteLine();
        }

        /// <summary>
        /// Gets the black list.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<string> GetBlackList()
        {
            return new List<string> 
            {
                "bad",
                "ugly",
                "danger"
            };
        }
    }
}
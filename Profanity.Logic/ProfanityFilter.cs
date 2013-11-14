/***************************************************************************************************************
 * 
 * The following class is based on James Newton-King blog post
 * Please visit: http://james.newtonking.com/archive/2009/07/03/simple-net-profanity-filter
 *
****************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Profanity.Logic
{
    public class ProfanityFilter
    {
        /// <summary>
        /// Gets the black list words.
        /// </summary>
        public IEnumerable<string> BlackListWords { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfanityFilter"/> class.
        /// </summary>
        /// <param name="blackListWords">The black list words.</param>
        public ProfanityFilter(IEnumerable<string> blackListWords)
        {
            this.BlackListWords = blackListWords;
        }

        /// <summary>
        /// Validates the text contains profanity.
        /// </summary>
        /// <param name="text">The text to validate.</param>
        /// <returns>True if the text contains profanity; false otherwise.</returns>
        public bool ValidateTextContainsProfanity(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Text is required");
            }

            bool textContainsProfanity = false;

            // Check if text contains each censored word in blacklist.
            foreach (var censoredWord in BlackListWords)
            {
                string regularExpression = ToRegexPattern(censoredWord);

                textContainsProfanity = Regex.IsMatch(text, regularExpression, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                if (textContainsProfanity)
                {
                    break;
                }
            }

            return textContainsProfanity;
        }

        /// <summary>
        /// Cleans the text profanity, replacing it with stars.
        /// </summary>
        /// <param name="text">The text to clean profanity.</param>
        /// <returns>The text without profanity words.</returns>
        /// <exception cref="System.ArgumentNullException">text</exception>
        public string CleanTextProfanity(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Text is required");
            }

            string censoredText = text;

            foreach (string censoredWord in BlackListWords)
            {
                string regularExpression = ToRegexPattern(censoredWord);

                censoredText = Regex.Replace(censoredText, regularExpression, StarCensoredMatch,
                  RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }

            return censoredText;
        }

        /// <summary>
        /// Converts a text into regex pattern.
        /// </summary>
        /// <param name="wildcardSearch">The wildcard search.</param>
        /// <returns>The regex pattern</returns>
        private string ToRegexPattern(string wildcardSearch)
        {
            string regexPattern = Regex.Escape(wildcardSearch);

            regexPattern = regexPattern.Replace(@"\*", ".*?");
            regexPattern = regexPattern.Replace(@"\?", ".");

            if (regexPattern.StartsWith(".*?"))
            {
                regexPattern = regexPattern.Substring(3);
                regexPattern = @"(^\b)*?" + regexPattern;
            }

            regexPattern = @"\b" + regexPattern + @"\b";

            return regexPattern;
        }

        /// <summary>
        /// Stars the censored match.
        /// </summary>
        /// <param name="m">The match text.</param>
        /// <returns>Censored text replaced with stars.</returns>
        private static string StarCensoredMatch(Match m)
        {
            string word = m.Captures[0].Value;

            return new string('*', word.Length);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentConsole
{
    public interface INegativeWord
    {
        bool NegativeWordExistsInDatastore(string word);
        void InsertNegativeWordIntoDatastore(string word);
        string[] RetrieveNegativeWordsFromDatastore();
    }

    public class BannedWords : INegativeWord
    {
        #region Constructors

        public BannedWords()
        {
            NegativeWords = new string[] { "swine", "bad", "nasty", "horrible" };
            NegativeWordsList = NegativeWords.ToList();
        }

        public BannedWords(string[] badWords)
        {
            NegativeWords = badWords;
            NegativeWordsList = NegativeWords.ToList();
        }

        public BannedWords(string contentToCheck, string[] badWords)
        {
            Content = contentToCheck;
            NegativeWords = badWords;
            NegativeWordsList = NegativeWords.ToList();
        }

        #endregion

        #region Properties

        public bool Result { get; set; }
        public string[] NegativeWords { get; set; }
        private List<string> NegativeWordsList { get; }
        public string Content
        {
            set
            {
                ContentArray = value.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        private string[] ContentArray { get; set; }

        #endregion

        public bool NegativeWordExistsInDatastore(string word)
        {
            return Result;
        }

        public void InsertNegativeWordIntoDatastore(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentException("Negative word cannot be empty");
            }
            NegativeWordsList.Add(word);
        }

        public string[] RetrieveNegativeWordsFromDatastore()
        {
            return NegativeWordsList.ToArray();
        }

        public string FilterOutNegativeWords()
        {
            string filteredWord;
            string filteredContent = string.Empty;
            List<string> filteredContentList = new List<string>();
            foreach (string word in ContentArray)
            {
                if (NegativeWords.Contains(word))
                {
                    filteredWord = FilterOutNegativeWord(word);
                    filteredContentList.Add(filteredWord);
                }
                else
                {
                    filteredContentList.Add(word);
                }
            }
            return string.Join(" ", filteredContentList);
        }

        public string FilterOutNegativeWord(string negativeWord)
        {
            string middle = negativeWord.Substring(1, negativeWord.Length - 2);
            return negativeWord.Replace(negativeWord.Substring(1, negativeWord.Length - 2), new string('#', middle.Length));
        }

        public long SumOfBannedWords()
        {
            int badWords = 0;
            if (ContentArray.Any(NegativeWords.Contains))
                badWords = ContentArray.Count(NegativeWords.Contains);

            return badWords;
        }
    }
}

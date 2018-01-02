using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class Stories
    {
        [TestCase("The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting." , new string[] { "swine", "bad", "nasty", "horrible" }, 2)]
        [TestCase("Charlie was a very bad dog and ate all the chocolate.", new string[] { "swine", "bad", "nasty", "horrible" }, 1)]
        [TestCase("Pirate Sam was a very nasty and horrible swine said the maiden.", new string[] { "swine", "bad", "nasty", "horrible" }, 3)]
        public void Test_Story1(string content, string[] words, long expected)
        {
            BannedWords bannedWords = new BannedWords();
            bannedWords.Content = content;
            bannedWords.NegativeWords = words;
            long badWords = bannedWords.SumOfBannedWords();
            
            Assert.AreEqual(expected, badWords);
        }

        [TestCase(new string[] { "swine", "bad", "nasty", "horrible" }, "terrible", new string[] { "swine", "bad", "nasty", "horrible", "terrible" })]
        public void Test_Story2(string[] negativeWords, string wordToAdd, string[] expected)
        {
            var mock = new Mock<INegativeWord>();
            mock.Setup(x => x.NegativeWordExistsInDatastore(It.IsAny<string>())).Returns(false);

            BannedWords bannedWords = new BannedWords();
            bannedWords.InsertNegativeWordIntoDatastore(wordToAdd);

            Assert.AreEqual(expected, bannedWords.RetrieveNegativeWordsFromDatastore());
        }

        [TestCase("horrible", "h######e")]
        [TestCase("terrible", "t######e")]
        [TestCase("bad", "b#d")]
        public void Test_Story3(string word, string expected)
        {
            BannedWords bannedWords = new BannedWords();

            Assert.AreEqual(expected, bannedWords.FilterOutNegativeWord(word));
        }

        [TestCase(
            "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.",
            new string[] { "swine", "bad", "nasty", "horrible" },
            "The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.")]
        public void Test_FilterOutNegativeWords(string content, string[] words, string expected)
        {
            BannedWords bannedWords = new BannedWords();
            bannedWords.Content = content;
            bannedWords.NegativeWords = words;

            Assert.AreEqual(expected, bannedWords.FilterOutNegativeWords());

        }
    }
}

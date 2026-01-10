using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Thainlp;

namespace ThaiNLPTest
{
    [TestClass]
    public class TrieTest
    {
        [TestMethod]
        public void TestTrieBasic()
        {
            // Test basic trie operations
            var words = new List<string> { "test", "testing", "tester", "tea" };
            var trie = new Trie(words);
            
            Assert.AreEqual(4, trie.Count);
            Assert.IsTrue(trie.Contains("test"));
            Assert.IsTrue(trie.Contains("testing"));
            Assert.IsTrue(trie.Contains("tester"));
            Assert.IsTrue(trie.Contains("tea"));
            Assert.IsFalse(trie.Contains("tes"));
        }

        [TestMethod]
        public void TestTriePrefixes()
        {
            // Test prefix matching
            var words = new List<string> { "test", "testing", "tester", "tea" };
            var trie = new Trie(words);
            
            var prefixes = trie.Prefixes("testing");
            Assert.AreEqual(2, prefixes.Count);
            CollectionAssert.Contains(prefixes, "test");
            CollectionAssert.Contains(prefixes, "testing");
        }

        [TestMethod]
        public void TestTrieThaiWords()
        {
            // Test with Thai words
            var words = new List<string> { "ประเทศ", "ประเทศไทย", "ไทย" };
            var trie = new Trie(words);
            
            Assert.AreEqual(3, trie.Count);
            Assert.IsTrue(trie.Contains("ประเทศ"));
            Assert.IsTrue(trie.Contains("ประเทศไทย"));
            Assert.IsTrue(trie.Contains("ไทย"));
            
            var prefixes = trie.Prefixes("ประเทศไทย");
            Assert.IsTrue(prefixes.Count >= 2);
            CollectionAssert.Contains(prefixes, "ประเทศ");
            CollectionAssert.Contains(prefixes, "ประเทศไทย");
        }

        [TestMethod]
        public void TestTrieAdd()
        {
            // Test adding words after creation
            var words = new List<string> { "test" };
            var trie = new Trie(words);
            
            Assert.AreEqual(1, trie.Count);
            trie.Add("testing");
            Assert.AreEqual(2, trie.Count);
            Assert.IsTrue(trie.Contains("testing"));
        }

        [TestMethod]
        public void TestTrieEmptyString()
        {
            // Test with empty string
            var words = new List<string> { "test", "" };
            var trie = new Trie(words);
            
            var prefixes = trie.Prefixes("");
            Assert.AreEqual(0, prefixes.Count);
        }

        [TestMethod]
        public void TestTrieNoPrefixes()
        {
            // Test when no prefixes match
            var words = new List<string> { "test", "testing" };
            var trie = new Trie(words);
            
            var prefixes = trie.Prefixes("xyz");
            Assert.AreEqual(0, prefixes.Count);
        }
    }
}

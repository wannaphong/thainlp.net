using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Thainlp;

namespace ThaiNLPTest
{
    [TestClass]
    public class NewMMTest
    {
        [TestMethod]
        public void TestNewMMBasic()
        {
            // Test basic Thai text tokenization
            string text = "ประเทศไทย";
            var tokens = WordTokenizer.WordTokenize(text);
            
            Assert.IsNotNull(tokens);
            Assert.IsTrue(tokens.Count >= 2);
            Assert.IsTrue(tokens.Contains("ประเทศ"));
            Assert.IsTrue(tokens.Contains("ไทย"));
        }

        [TestMethod]
        public void TestNewMMLongText()
        {
            // Test longer sentence
            string text = "ประเทศไทยมีอากาศดี";
            var tokens = WordTokenizer.WordTokenize(text);
            
            Assert.IsNotNull(tokens);
            Assert.IsTrue(tokens.Count >= 5);
            CollectionAssert.Contains(tokens, "ประเทศ");
            CollectionAssert.Contains(tokens, "ไทย");
            CollectionAssert.Contains(tokens, "มี");
            CollectionAssert.Contains(tokens, "อากาศ");
            CollectionAssert.Contains(tokens, "ดี");
        }

        [TestMethod]
        public void TestNewMMComplexText()
        {
            // Test from PyThaiNLP example
            string text = "โอเคบ่พวกเรารักภาษาบ้านเกิด";
            var tokens = WordTokenizer.WordTokenize(text);
            
            Assert.IsNotNull(tokens);
            CollectionAssert.Contains(tokens, "โอเค");
            CollectionAssert.Contains(tokens, "บ่");
            CollectionAssert.Contains(tokens, "พวกเรา");
            CollectionAssert.Contains(tokens, "รัก");
            CollectionAssert.Contains(tokens, "ภาษา");
            CollectionAssert.Contains(tokens, "บ้านเกิด");
        }

        [TestMethod]
        public void TestNewMMEmptyString()
        {
            // Test empty string
            string text = "";
            var tokens = WordTokenizer.WordTokenize(text);
            
            Assert.IsNotNull(tokens);
            Assert.AreEqual(0, tokens.Count);
        }

        [TestMethod]
        public void TestNewMMNullString()
        {
            // Test null string
            string text = null;
            var tokens = WordTokenizer.WordTokenize(text);
            
            Assert.IsNotNull(tokens);
            Assert.AreEqual(0, tokens.Count);
        }

        [TestMethod]
        public void TestNewMMKeepWhitespace()
        {
            // Test keeping whitespace
            string text = "วรรณกรรม ภาพวาด";
            var tokensWithWhitespace = WordTokenizer.WordTokenize(text, keepWhitespace: true);
            var tokensWithoutWhitespace = WordTokenizer.WordTokenize(text, keepWhitespace: false);
            
            Assert.IsTrue(tokensWithWhitespace.Count > tokensWithoutWhitespace.Count);
            Assert.IsFalse(tokensWithoutWhitespace.Any(t => string.IsNullOrWhiteSpace(t)));
        }

        [TestMethod]
        public void TestNewMMConvenienceMethod()
        {
            // Test the convenience Tokenize method
            string text = "ประเทศไทย";
            var tokens = WordTokenizer.Tokenize(text);
            
            Assert.IsNotNull(tokens);
            Assert.IsTrue(tokens.Count >= 2);
        }

        [TestMethod]
        public void TestNewMMEngine()
        {
            // Test explicit engine parameter
            string text = "ประเทศไทย";
            var tokens = WordTokenizer.WordTokenize(text, engine: "newmm");
            
            Assert.IsNotNull(tokens);
            Assert.IsTrue(tokens.Count >= 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNewMMInvalidEngine()
        {
            // Test invalid engine parameter
            string text = "ประเทศไทย";
            WordTokenizer.WordTokenize(text, engine: "invalid");
        }
    }
}

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

        [TestMethod]
        public void TestNewMMComprehensive()
        {
            // Comprehensive test from PyThaiNLP
            // Test null input
            var result1 = NewMM.Segment(null);
            CollectionAssert.AreEqual(new List<string>(), result1);

            // Test empty string
            var result2 = NewMM.Segment("");
            CollectionAssert.AreEqual(new List<string>(), result2);

            // Test Thai sentence tokenization
            var result3 = WordTokenizer.WordTokenize("ฉันรักภาษาไทยเพราะฉันเป็นคนไทย", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "ฉัน", "รัก", "ภาษาไทย", "เพราะ", "ฉัน", "เป็น", "คนไทย" },
                result3
            );

            // Test numeric patterns - dots
            var result4 = WordTokenizer.WordTokenize("19...", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "19", "..." },
                result4
            );

            // Test numeric patterns - single dot
            var result5 = WordTokenizer.WordTokenize("19.", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "19", "." },
                result5
            );

            // Test numeric patterns - decimal
            var result6 = WordTokenizer.WordTokenize("19.84", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "19.84" },
                result6
            );

            // Test numeric patterns - IP address
            var result7 = WordTokenizer.WordTokenize("127.0.0.1", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "127.0.0.1" },
                result7
            );

            // Test numeric patterns - currency
            var result8 = WordTokenizer.WordTokenize("USD1,984.42", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "USD", "1,984.42" },
                result8
            );

            // Test keep_whitespace parameter
            var result9 = WordTokenizer.WordTokenize(
                "สวัสดีครับ สบายดีไหมครับ",
                engine: "newmm",
                keepWhitespace: true
            );
            CollectionAssert.AreEqual(
                new List<string> { "สวัสดี", "ครับ", " ", "สบายดี", "ไหม", "ครับ" },
                result9
            );

            // Test Thai text with uncommon words
            var result10 = WordTokenizer.WordTokenize("จุ๋มง่วงนอนยัง", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "จุ๋ม", "ง่วงนอน", "ยัง" },
                result10
            );

            // Test Thai text
            var result11 = WordTokenizer.WordTokenize("จุ๋มง่วง", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "จุ๋ม", "ง่วง" },
                result11
            );

            // Test whitespace handling with keep_whitespace=false
            var result12 = WordTokenizer.WordTokenize("จุ๋ม   ง่วง", engine: "newmm", keepWhitespace: false);
            CollectionAssert.AreEqual(
                new List<string> { "จุ๋ม", "ง่วง" },
                result12
            );

            // Test that whitespace is not included when keep_whitespace=false
            var result13 = WordTokenizer.WordTokenize("จุ๋มง่วง", keepWhitespace: false);
            CollectionAssert.DoesNotContain(result13, " ");

            // Test parentheses
            var result14 = WordTokenizer.WordTokenize("(คนไม่เอา)", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "(", "คน", "ไม่", "เอา", ")" },
                result14
            );

            // Test slash
            var result15 = WordTokenizer.WordTokenize("กม/ชม", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "กม", "/", "ชม" },
                result15
            );

            // Test complex case with Thai and parentheses
            var result16 = WordTokenizer.WordTokenize("สีหน้า(รถ)", engine: "newmm");
            CollectionAssert.AreEqual(
                new List<string> { "สีหน้า", "(", "รถ", ")" },
                result16
            );
        }
    }
}

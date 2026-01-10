using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Thainlp;

namespace ThaiNLPTest
{
    [TestClass]
    public class TCCTest
    {
        [TestMethod]
        public void TestTCCBasic()
        {
            // Test basic TCC tokenization
            string text = "ประเทศไทย";
            var tokens = TCC.Segment(text);
            
            Assert.IsNotNull(tokens);
            Assert.IsTrue(tokens.Length > 0);
        }

        [TestMethod]
        public void TestTCCConsistency()
        {
            // Test TCC tokenization is consistent
            string text = "ประเทศไทย";
            var tccResult1 = TCC.Segment(text);
            var tccResult2 = TCC.Segment(text);
            
            CollectionAssert.AreEqual(tccResult1, tccResult2);
        }

        [TestMethod]
        public void TestTCCPositions()
        {
            // Test TCC position detection
            string text = "ประเทศไทย";
            var positions = TCC.GetPositions(text);
            
            Assert.IsNotNull(positions);
            Assert.IsTrue(positions.Count > 0);
            Assert.IsTrue(positions.Contains(text.Length));
        }

        [TestMethod]
        public void TestTCCEmpty()
        {
            // Test TCC with empty string
            var clusters = TCC.Segment("");
            Assert.IsNotNull(clusters);
            Assert.AreEqual(0, clusters.Length);
        }

        [TestMethod]
        public void TestTCCPositionCalculation()
        {
            // Test that TCC positions are correctly calculated
            string text = "กรุงเทพ";
            var positions = TCC.GetPositions(text);
            
            Assert.IsNotNull(positions);
            Assert.IsTrue(positions.Count > 0);
            // The last position should always be the text length
            Assert.IsTrue(positions.Contains(text.Length));
        }
    }
}

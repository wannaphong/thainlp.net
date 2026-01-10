using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Thainlp;
namespace ThaiNLPTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSubwordTCC()
        {
            // Test original Subword.tcc implementation
            string txt = "ประเทศไทย";
            string[] tcc_output = Subword.tcc(txt);
            
            Assert.IsNotNull(tcc_output);
            Assert.IsTrue(tcc_output.Length > 0);
            
            // Verify it produces consistent results
            string[] tcc_output2 = Subword.tcc(txt);
            CollectionAssert.AreEqual(tcc_output, tcc_output2);
        }

        [TestMethod]
        public void TestSubwordTCCPos()
        {
            // Test tcc_pos function
            string txt = "ประเทศไทย";
            int[] positions = Subword.tcc_pos(txt);
            
            Assert.IsNotNull(positions);
            Assert.IsTrue(positions.Length > 0);
            // Last position should be text length
            Assert.AreEqual(txt.Length, positions[positions.Length - 1]);
        }
    }
}

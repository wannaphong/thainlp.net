using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thainlp;
namespace ThaiNLPTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            string txt = "ประเทศไทย";
            string[] tcc = new string[6] {"ป","ระ","เท","ศ","ไท","ย"};
            string[] tcc_output = Subword.tcc(txt);
            Assert.AreEqual(tcc_output, tcc);
        }
    }
}

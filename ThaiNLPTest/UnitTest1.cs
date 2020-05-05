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
            string[] tcc = new string[] {"ป","ระ","เท","ศ","ไท","ย"};
            Assert.AreEqual(Subword.tcc(txt), tcc);
        }
    }
}

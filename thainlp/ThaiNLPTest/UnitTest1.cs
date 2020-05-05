using Microsoft.VisualStudio.TestTools.UnitTesting;
using thainlp;
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
            Assert.Equals(Subword.tcc(txt), tcc);
        }
    }
}

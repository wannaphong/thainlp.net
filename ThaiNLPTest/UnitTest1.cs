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

            string txt = "�������";
            string[] tcc = new string[] {"�","��","�","�","�","�"};
            Assert.AreEqual(Subword.tcc(txt), tcc);
        }
    }
}

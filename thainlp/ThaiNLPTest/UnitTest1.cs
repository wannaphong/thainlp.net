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

            string txt = "�������";
            string[] tcc = new string[] {"�","��","�","�","�","�"};
            Assert.Equals(Subword.tcc(txt), tcc);
        }
    }
}

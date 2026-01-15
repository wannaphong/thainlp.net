using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thainlp;

namespace ThaiNLPTest
{
    [TestClass]
    public class NumToWordTest
    {
        [TestMethod]
        public void TestBahtText()
        {
            Assert.AreEqual(
                "ห้าล้านหกแสนหนึ่งหมื่นหนึ่งพันหนึ่งร้อยสิบหกบาทห้าสิบสตางค์",
                NumToWord.BahtText(5611116.50)
            );
            Assert.AreEqual("หนึ่งร้อยสิบหกบาทถ้วน", NumToWord.BahtText(116));
            Assert.AreEqual("ศูนย์บาทถ้วน", NumToWord.BahtText(0));
            Assert.AreEqual("", NumToWord.BahtText(null));
        }

        [TestMethod]
        public void TestNumToThaiWord()
        {
            Assert.AreEqual("", NumToWord.NumToThaiWord(null));
            Assert.AreEqual("ศูนย์", NumToWord.NumToThaiWord(0));
            Assert.AreEqual("หนึ่งร้อยสิบสอง", NumToWord.NumToThaiWord(112));
            Assert.AreEqual("ลบสองร้อยเจ็ดสิบสาม", NumToWord.NumToThaiWord(-273));
        }
    }
}

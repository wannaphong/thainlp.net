using System;
using System.Collections.Generic;

namespace Thainlp
{
    /// <summary>
    /// Convert number value to Thai read out.
    /// Adapted from PyThaiNLP's num_to_thaiword implementation.
    /// </summary>
    public class NumToWord
    {
        private static readonly string[] _VALUES = new[]
        {
            "",
            "หนึ่ง",
            "สอง",
            "สาม",
            "สี่",
            "ห้า",
            "หก",
            "เจ็ด",
            "แปด",
            "เก้า"
        };

        private static readonly string[] _PLACES = new[]
        {
            "",
            "สิบ",
            "ร้อย",
            "พัน",
            "หมื่น",
            "แสน",
            "ล้าน"
        };

        private static readonly Dictionary<string, string> _EXCEPTIONS = new Dictionary<string, string>
        {
            { "หนึ่งสิบ", "สิบ" },
            { "สองสิบ", "ยี่สิบ" },
            { "สิบหนึ่ง", "สิบเอ็ด" }
        };

        /// <summary>
        /// This function converts a number to Thai text and adds
        /// a suffix "บาท" (Baht).
        /// The precision will be fixed at two decimal places (0.00)
        /// to fits "สตางค์" (Satang) unit.
        /// This function works similar to BAHTTEXT function in Microsoft Excel.
        /// </summary>
        /// <param name="number">Number to be converted into Thai Baht currency format</param>
        /// <returns>Text representing the amount of money in the format of Thai currency</returns>
        /// <example>
        /// <code>
        /// NumToWord.BahtText(1)
        /// // output: หนึ่งบาทถ้วน
        /// 
        /// NumToWord.BahtText(21)
        /// // output: ยี่สิบเอ็ดบาทถ้วน
        /// 
        /// NumToWord.BahtText(200)
        /// // output: สองร้อยบาทถ้วน
        /// </code>
        /// </example>
        public static string BahtText(double? number)
        {
            string ret = "";

            if (number == null)
            {
                return ret;
            }
            else if (number == 0)
            {
                ret = "ศูนย์บาทถ้วน";
            }
            else
            {
                string formatted = number.Value.ToString("F2");
                string[] parts = formatted.Split('.');
                int numInt = int.Parse(parts[0]);
                int numDec = int.Parse(parts[1]);

                string baht = NumToThaiWord(numInt);
                if (!string.IsNullOrEmpty(baht))
                {
                    ret = ret + baht + "บาท";
                }

                string satang = NumToThaiWord(numDec);
                if (!string.IsNullOrEmpty(satang) && satang != "ศูนย์")
                {
                    ret = ret + satang + "สตางค์";
                }
                else
                {
                    ret = ret + "ถ้วน";
                }
            }

            return ret;
        }

        /// <summary>
        /// This function converts number to Thai text.
        /// </summary>
        /// <param name="number">An integer number to be converted to Thai text</param>
        /// <returns>Text representing the number in Thai</returns>
        /// <example>
        /// <code>
        /// NumToWord.NumToThaiWord(1)
        /// // output: หนึ่ง
        /// 
        /// NumToWord.NumToThaiWord(11)
        /// // output: สิบเอ็ด
        /// </code>
        /// </example>
        public static string NumToThaiWord(int? number)
        {
            string output = "";

            if (number == null)
            {
                return output;
            }
            else if (number == 0)
            {
                output = "ศูนย์";
                return output;
            }

            int numberTemp = number.Value;
            string numberStr = Math.Abs(number.Value).ToString();
            char[] digits = numberStr.ToCharArray();
            Array.Reverse(digits);

            for (int place = 0; place < digits.Length; place++)
            {
                if (place % 6 == 0 && place > 0)
                {
                    output = _PLACES[6] + output;
                }

                if (digits[place] != '0')
                {
                    int value = int.Parse(digits[place].ToString());
                    output = _VALUES[value] + _PLACES[place % 6] + output;
                }
            }

            foreach (var exception in _EXCEPTIONS)
            {
                output = output.Replace(exception.Key, exception.Value);
            }

            if (numberTemp < 0)
            {
                output = "ลบ" + output;
            }

            return output;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace thainlp
{
    class Subword
    {
        public static string[] tcc(string txt)
        {
            String re = @"เ[ก-ฮ]็[ก-ฮ]|เ[ก-ฮ][ก-ฮ][่-๋]?าะ|เ[ก-ฮ][ก-ฮ]ี[่-๋]?ยะ|เ[ก-ฮ][ก-ฮ]ี[่-๋]?ย(?=[เ-ไก-ฮ]|$)|เ[ก-ฮ][ก-ฮ]็[ก-ฮ]|เ[ก-ฮ]ิ[ก-ฮ]์[ก-ฮ]|เ[ก-ฮ]ิ[่-๋]?[ก-ฮ]|เ[ก-ฮ]ี[่-๋]?ยะ?|เ[ก-ฮ]ื[่-๋]?อะ?|เ[ก-ฮ][ิีุู][่-๋]?ย(?=[เ-ไก-ฮ]|$)|เ[ก-ฮ][่-๋]?า?ะ?|[ก-ฮ]ั[่-๋]?วะ|[ก-ฮ][ัื][่-๋]?[ก-ฮ][ุิะ]?|[ก-ฮ][ิุู]์|[ก-ฮ][ะ-ู][่-๋]?|[ก-ฮ]็|[ก-ฮ][่-๋]?[ะาำ]?|แ[ก-ฮ]็[ก-ฮ]|แ[ก-ฮ][ก-ฮ]์|แ[ก-ฮ][่-๋]?ะ|แ[ก-ฮ][ก-ฮ]็[ก-ฮ]|แ[ก-ฮ][ก-ฮ][ก-ฮ]์|โ[ก-ฮ][่-๋]?ะ|[เ-ไ][ก-ฮ][่-๋]?";
            int p = 0;
            int n = 0;
            int temp = 0;
            List<String> d = new List<String>();
            while (p < txt.Length)
            {
                Console.WriteLine("c : " + txt[p..]);
                Match mc = Regex.Match(txt[p..], re);
                if (mc.Success)
                {
                    n = mc.Value.Length;
                    Console.WriteLine(n);
                }
                else
                {
                    n = 1;
                }
                temp = p + n;
                d.Add(txt[p..temp]);
                p += n;
            }
            foreach (string i in d)
            {
                Console.WriteLine(i);
            }
            return d.ToArray();
        }
    }
}

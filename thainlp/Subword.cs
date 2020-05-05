using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Thainlp
{
    public class Subword
    {
        public static string[] tcc(string txt)
        {
            string re = @"เ[ก-ฮ]็[ก-ฮ]|เ[ก-ฮ][ก-ฮ][่-๋]?าะ|เ[ก-ฮ][ก-ฮ]ี[่-๋]?ยะ|เ[ก-ฮ][ก-ฮ]ี[่-๋]?ย(?=[เ-ไก-ฮ]|$)|เ[ก-ฮ][ก-ฮ]็[ก-ฮ]|เ[ก-ฮ]ิ[ก-ฮ]์[ก-ฮ]|เ[ก-ฮ]ิ[่-๋]?[ก-ฮ]|เ[ก-ฮ]ี[่-๋]?ยะ?|เ[ก-ฮ]ื[่-๋]?อะ?|เ[ก-ฮ][ิีุู][่-๋]?ย(?=[เ-ไก-ฮ]|$)|เ[ก-ฮ][่-๋]?า?ะ?|[ก-ฮ]ั[่-๋]?วะ|[ก-ฮ][ัื][่-๋]?[ก-ฮ][ุิะ]?|[ก-ฮ][ิุู]์|[ก-ฮ][ะ-ู][่-๋]?|[ก-ฮ]็|[ก-ฮ][่-๋]?[ะาำ]?|แ[ก-ฮ]็[ก-ฮ]|แ[ก-ฮ][ก-ฮ]์|แ[ก-ฮ][่-๋]?ะ|แ[ก-ฮ][ก-ฮ]็[ก-ฮ]|แ[ก-ฮ][ก-ฮ][ก-ฮ]์|โ[ก-ฮ][่-๋]?ะ|[เ-ไ][ก-ฮ][่-๋]?";
            int p = 0;
            int n = 0;
            int temp = 0;
            List<string> d = new List<string>();
            while (p < txt.Length)
            {
               // Console.WriteLine("c : " + txt[p..]);
                Match mc = Regex.Match(txt[p..], re);
                if (mc.Success)
                {
                    n = mc.Value.Length;
                 //   Console.WriteLine(n);
                }
                else
                {
                    n = 1;
                }
                temp = p + n;
                d.Add(txt[p..temp]);
                p += n;
            }
            
            return d.ToArray();
        }
        public static int[] tcc_pos(string text)
        {
            List<int> p_set = new List<int>();
            int p = 0;
            foreach(string w in tcc(text))
            {
                p += w.Length;
                p_set.Add(p);
            }
            return p_set.ToArray();
        }
    }
}

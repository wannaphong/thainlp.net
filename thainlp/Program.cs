using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Thainlp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("ทดสอบระบบ ThaiNLP.NET");
            Console.WriteLine();
            
            // Test TCC tokenizer
            Console.WriteLine("=== TCC Tokenizer Test ===");
            string text1 = "ประเทศไทย";
            var tccResult = Subword.tcc(text1);
            Console.WriteLine($"Input: {text1}");
            Console.WriteLine($"TCC Output: {string.Join("|", tccResult)}");
            Console.WriteLine();
            
            // Test newmm tokenizer
            Console.WriteLine("=== NewMM Tokenizer Test ===");
            string text2 = "ประเทศไทยมีอากาศดี";
            var tokens = WordTokenizer.WordTokenize(text2);
            Console.WriteLine($"Input: {text2}");
            Console.WriteLine($"Tokens: {string.Join("|", tokens)}");
            Console.WriteLine();
            
            // Another test
            string text3 = "โอเคบ่พวกเรารักภาษาบ้านเกิด";
            tokens = WordTokenizer.Tokenize(text3);
            Console.WriteLine($"Input: {text3}");
            Console.WriteLine($"Tokens: {string.Join("|", tokens)}");
        }
    }
}

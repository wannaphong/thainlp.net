using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Thainlp
{
    /// <summary>
    /// Thai Character Cluster (TCC) tokenizer.
    /// Implementation based on rules proposed by Theeramunkong et al. 2000
    /// with improved rules used in PyThaiNLP's newmm tokenizer.
    /// </summary>
    public class TCC
    {
        private static readonly Regex _patternTCC;

        static TCC()
        {
            // Build TCC regex pattern based on PyThaiNLP's tcc_p.py
            // This represents Thai character cluster rules
            string[] patterns = new[]
            {
                @"เ[ก-ฮ]็[ก-ฮ]([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ][ก-ฮ][่-๋]?าะ([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ][ก-ฮ]ี[่-๋]?ยะ([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ][ก-ฮ]ี[่-๋]?ย(?=[เ-ไก-ฮ]|$)([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ][ก-ฮ]็[ก-ฮ]([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ]ิ[ก-ฮ]์[ก-ฮ]([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ]ิ[่-๋]?[ก-ฮ]([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ]ี[่-๋]?ยะ?([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ]ื[่-๋]?อะ?([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ][ิีุู][่-๋]?ย(?=[เ-ไก-ฮ]|$)([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"เ[ก-ฮ][่-๋]?า?ะ?([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"[ก-ฮ]ั[่-๋]?วะ([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"[ก-ฮ][ัื][่-๋]?[ก-ฮ][ุิะ]?([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"[ก-ฮ][ิุู]์",
                @"[ก-ฮ][ะ-ู][่-๋]?([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"[ก-ฮ]รร[ก-ฮ]์",
                @"[ก-ฮ]็",
                @"[ก-ฮ][่-๋]?[ะาำ]?([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"([ก-ฮ]{0,2}[ิุ]?[์])?",
                @"แ[ก-ฮ]็[ก-ฮ]",
                @"แ[ก-ฮ][ก-ฮ]์",
                @"แ[ก-ฮ][่-๋]?ะ",
                @"แ[ก-ฮ][ก-ฮ]็[ก-ฮ]",
                @"แ[ก-ฮ][ก-ฮ][ก-ฮ]์",
                @"โ[ก-ฮ][่-๋]?ะ",
                @"[เ-ไ][ก-ฮ][่-๋]?",
                @"ก็",
                @"อึ",
                @"หึ"
            };

            _patternTCC = new Regex(string.Join("|", patterns), RegexOptions.Compiled);
        }

        /// <summary>
        /// Tokenize text into Thai Character Clusters.
        /// </summary>
        /// <param name="text">Text to tokenize</param>
        /// <returns>Array of character clusters</returns>
        public static string[] Segment(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Array.Empty<string>();

            var result = new List<string>();
            int pos = 0;
            int textLength = text.Length;

            while (pos < textLength)
            {
                var match = _patternTCC.Match(text, pos);
                int length;
                
                if (match.Success && match.Index == pos && match.Length > 0)
                {
                    length = match.Length;
                }
                else
                {
                    length = 1;
                }

                result.Add(text.Substring(pos, length));
                pos += length;
            }

            return result.ToArray();
        }

        /// <summary>
        /// Get positions where Thai Character Clusters end.
        /// Returns a set of valid breaking positions.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>Set of ending positions</returns>
        public static HashSet<int> GetPositions(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new HashSet<int>();

            var positions = new HashSet<int>();
            int pos = 0;

            foreach (var cluster in Segment(text))
            {
                pos += cluster.Length;
                positions.Add(pos);
            }

            return positions;
        }
    }
}

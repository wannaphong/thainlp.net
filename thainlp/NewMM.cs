using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Thainlp
{
    /// <summary>
    /// Dictionary-based maximal matching word segmentation constrained by
    /// Thai Character Cluster (TCC) boundaries with improved rules.
    /// Based on PyThaiNLP's newmm tokenizer.
    /// </summary>
    public class NewMM
    {
        private const int MaxGraphSize = 50;
        private static readonly Regex NonThaiPattern;
        private static readonly Regex TwoCharsPattern;

        static NewMM()
        {
            // Match non-Thai tokens
            NonThaiPattern = new Regex(
                @"[-a-zA-Z]+|\d+([,\.]\d+)*|[ \t]+|\r?\n|[^\u0E00-\u0E7F \t\r\n]+",
                RegexOptions.Compiled
            );

            // Match 2-consonant Thai tokens
            TwoCharsPattern = new Regex(@"[ก-ฮ]{1,2}$", RegexOptions.Compiled);
        }

        private class GraphPath
        {
            public int Vertex { get; set; }
            public List<int> Path { get; set; }
        }

        /// <summary>
        /// Find all paths in the graph from start to goal using BFS.
        /// </summary>
        private static IEnumerable<List<int>> BfsPathsGraph(
            Dictionary<int, List<int>> graph, int start, int goal)
        {
            var queue = new Queue<GraphPath>();
            queue.Enqueue(new GraphPath { Vertex = start, Path = new List<int> { start } });

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                
                if (graph.ContainsKey(current.Vertex))
                {
                    foreach (var pos in graph[current.Vertex])
                    {
                        if (pos == goal)
                        {
                            var newPath = new List<int>(current.Path) { pos };
                            yield return newPath;
                        }
                        else
                        {
                            var newPath = new List<int>(current.Path) { pos };
                            queue.Enqueue(new GraphPath { Vertex = pos, Path = newPath });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Internal method to perform one-cut tokenization.
        /// </summary>
        private static IEnumerable<string> OneCut(string text, Trie customDict)
        {
            var graph = new Dictionary<int, List<int>>();
            int graphSize = 0;

            var validPos = TCC.GetPositions(text);
            int textLength = text.Length;
            
            var posList = new List<int> { 0 };
            int endPos = 0;

            while (posList.Count > 0 && posList[0] < textLength)
            {
                posList.Sort();
                int beginPos = posList[0];
                posList.RemoveAt(0);

                var prefixes = customDict.Prefixes(text.Substring(beginPos));
                
                foreach (var word in prefixes)
                {
                    int endPosCandidate = beginPos + word.Length;
                    
                    if (validPos.Contains(endPosCandidate))
                    {
                        if (!graph.ContainsKey(beginPos))
                            graph[beginPos] = new List<int>();
                        
                        graph[beginPos].Add(endPosCandidate);
                        graphSize++;

                        if (!posList.Contains(endPosCandidate))
                        {
                            posList.Add(endPosCandidate);
                        }

                        if (graphSize > MaxGraphSize)
                            break;
                    }
                }

                int posListLen = posList.Count;
                
                if (posListLen == 1)
                {
                    // One candidate, no longer ambiguous
                    var paths = BfsPathsGraph(graph, endPos, posList[0]).FirstOrDefault();
                    if (paths != null)
                    {
                        graphSize = 0;
                        for (int i = 1; i < paths.Count; i++)
                        {
                            yield return text.Substring(endPos, paths[i] - endPos);
                            endPos = paths[i];
                        }
                    }
                }
                else if (posListLen == 0)
                {
                    // No candidate, deal with non-dictionary word
                    var match = NonThaiPattern.Match(text, beginPos);
                    
                    if (match.Success && match.Index == beginPos)
                    {
                        // Non-Thai token, skip to the end
                        endPos = beginPos + match.Length;
                    }
                    else
                    {
                        // Thai token, find minimum skip
                        endPos = textLength;
                        for (int pos = beginPos + 1; pos < textLength; pos++)
                        {
                            if (validPos.Contains(pos))
                            {
                                string prefix = text.Substring(pos);
                                var words = customDict.Prefixes(prefix)
                                    .Where(w => validPos.Contains(pos + w.Length) && 
                                               !TwoCharsPattern.IsMatch(w))
                                    .ToList();

                                if (words.Count > 0)
                                {
                                    endPos = pos;
                                    break;
                                }

                                if (NonThaiPattern.IsMatch(prefix))
                                {
                                    endPos = pos;
                                    break;
                                }
                            }
                        }
                    }

                    if (!graph.ContainsKey(beginPos))
                        graph[beginPos] = new List<int>();
                    
                    graph[beginPos].Add(endPos);
                    graphSize++;
                    
                    yield return text.Substring(beginPos, endPos - beginPos);
                    posList.Add(endPos);
                }
            }

            // Output remaining tokens from endPos to textLength
            if (endPos < textLength)
            {
                var paths = BfsPathsGraph(graph, endPos, textLength).FirstOrDefault();
                if (paths != null)
                {
                    for (int i = 1; i < paths.Count; i++)
                    {
                        yield return text.Substring(endPos, paths[i] - endPos);
                        endPos = paths[i];
                    }
                }
            }
        }

        /// <summary>
        /// Tokenize Thai text using maximal matching constrained by TCC boundaries.
        /// This is the main newmm tokenizer method.
        /// </summary>
        /// <param name="text">Text to tokenize</param>
        /// <param name="customDict">Custom dictionary (Trie). If null, default dictionary is used.</param>
        /// <returns>List of tokens</returns>
        public static List<string> Segment(string text, Trie customDict = null)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            if (customDict == null)
            {
                customDict = WordTokenizer.GetDefaultDict();
            }

            return OneCut(text, customDict).ToList();
        }
    }
}

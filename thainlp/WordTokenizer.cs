using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Thainlp
{
    /// <summary>
    /// Word tokenizer for Thai text.
    /// Provides an API similar to PyThaiNLP's word_tokenize function.
    /// </summary>
    public class WordTokenizer
    {
        private static Trie _defaultDict = null;
        private static readonly object _lock = new object();

        /// <summary>
        /// Get the default Thai word dictionary.
        /// Lazy loads the dictionary from embedded resource.
        /// </summary>
        /// <returns>Default Trie dictionary</returns>
        public static Trie GetDefaultDict()
        {
            if (_defaultDict == null)
            {
                lock (_lock)
                {
                    if (_defaultDict == null)
                    {
                        _defaultDict = LoadDefaultDict();
                    }
                }
            }
            return _defaultDict;
        }

        /// <summary>
        /// Load the default Thai word dictionary from file.
        /// </summary>
        private static Trie LoadDefaultDict()
        {
            var words = new List<string>();
            
            // Try to load from file next to the assembly
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dictPath = Path.Combine(assemblyPath, "words_th.txt");
            
            // Alternative: check current directory
            if (!File.Exists(dictPath))
            {
                dictPath = "words_th.txt";
            }
            
            // Alternative: check in thainlp subdirectory
            if (!File.Exists(dictPath))
            {
                dictPath = Path.Combine("thainlp", "words_th.txt");
            }

            if (File.Exists(dictPath))
            {
                foreach (var line in File.ReadLines(dictPath))
                {
                    var word = line.Trim();
                    if (!string.IsNullOrEmpty(word))
                    {
                        words.Add(word);
                    }
                }
            }
            else
            {
                // If no dictionary file found, return empty trie
                Console.WriteLine($"Warning: Dictionary file not found at {dictPath}");
            }

            return new Trie(words);
        }

        /// <summary>
        /// Tokenize Thai text into words.
        /// Similar to PyThaiNLP's word_tokenize function.
        /// </summary>
        /// <param name="text">Text to tokenize</param>
        /// <param name="engine">Tokenization engine (default: "newmm")</param>
        /// <param name="customDict">Custom dictionary for tokenization</param>
        /// <param name="keepWhitespace">Whether to keep whitespace tokens (default: true)</param>
        /// <returns>List of word tokens</returns>
        public static List<string> WordTokenize(
            string text,
            string engine = "newmm",
            Trie customDict = null,
            bool keepWhitespace = true)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            List<string> tokens;

            switch (engine.ToLower())
            {
                case "newmm":
                    tokens = NewMM.Segment(text, customDict);
                    break;
                default:
                    throw new ArgumentException($"Unknown engine: {engine}");
            }

            if (!keepWhitespace)
            {
                tokens = tokens.Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            }

            return tokens;
        }

        /// <summary>
        /// Tokenize Thai text into words using the default newmm engine.
        /// Convenience method with fewer parameters.
        /// </summary>
        /// <param name="text">Text to tokenize</param>
        /// <returns>List of word tokens</returns>
        public static List<string> Tokenize(string text)
        {
            return WordTokenize(text);
        }
    }
}

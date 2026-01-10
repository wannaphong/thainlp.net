using System;
using System.Collections.Generic;
using System.Linq;

namespace Thainlp
{
    /// <summary>
    /// Trie data structure for efficient prefix matching.
    /// Used for dictionary-based tokenization.
    /// </summary>
    public class Trie
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children { get; set; }
            public bool IsEndOfWord { get; set; }

            public TrieNode()
            {
                Children = new Dictionary<char, TrieNode>();
                IsEndOfWord = false;
            }
        }

        private readonly TrieNode root;
        private readonly HashSet<string> words;

        public Trie(IEnumerable<string> words)
        {
            root = new TrieNode();
            this.words = new HashSet<string>();
            
            foreach (var word in words)
            {
                Add(word);
            }
        }

        /// <summary>
        /// Add a word to the trie.
        /// Spaces in front of and following the word will be removed.
        /// </summary>
        /// <param name="word">Word to add</param>
        public void Add(string word)
        {
            if (string.IsNullOrEmpty(word))
                return;

            word = word.Trim();
            words.Add(word);

            var current = root;
            foreach (char ch in word)
            {
                if (!current.Children.ContainsKey(ch))
                {
                    current.Children[ch] = new TrieNode();
                }
                current = current.Children[ch];
            }
            current.IsEndOfWord = true;
        }

        /// <summary>
        /// Check if a word exists in the trie.
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns>True if word exists, false otherwise</returns>
        public bool Contains(string word)
        {
            return words.Contains(word);
        }

        /// <summary>
        /// List all possible words from first sequence of characters in a text.
        /// Returns all valid prefixes that form complete words in the dictionary.
        /// </summary>
        /// <param name="text">Text to find prefixes from</param>
        /// <returns>List of valid word prefixes</returns>
        public List<string> Prefixes(string text)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(text))
                return result;

            var current = root;
            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if (!current.Children.ContainsKey(ch))
                    break;

                current = current.Children[ch];
                if (current.IsEndOfWord)
                {
                    result.Add(text.Substring(0, i + 1));
                }
            }

            return result;
        }

        /// <summary>
        /// Get the number of words in the trie.
        /// </summary>
        public int Count => words.Count;
    }
}

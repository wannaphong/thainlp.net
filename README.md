# thainlp.net
Thai NLP in .NET

## Features

### Word Tokenization
- **newmm** - Dictionary-based maximal matching word segmentation constrained by Thai Character Cluster (TCC) boundaries
- API similar to PyThaiNLP for easy migration from Python

### Subword Tokenization
- **TCC** (Thai Character Cluster) tokenization for breaking text into character clusters

## Installation

### From NuGet (Recommended)

```bash
dotnet add package ThaiNLP.NET
```

Or via Package Manager:
```
Install-Package ThaiNLP.NET
```

### From Source

Build the project:
```bash
dotnet build
```

## Usage

### Word Tokenization (newmm)

Basic usage:
```csharp
using Thainlp;

// Simple tokenization
var tokens = WordTokenizer.Tokenize("ประเทศไทยมีอากาศดี");
// Output: ["ประเทศ", "ไทย", "มี", "อากาศ", "ดี"]

// With more options
var tokens = WordTokenizer.WordTokenize(
    text: "โอเคบ่พวกเรารักภาษาบ้านเกิด",
    engine: "newmm",
    keepWhitespace: true
);
// Output: ["โอเค", "บ่", "พวกเรา", "รัก", "ภาษา", "บ้านเกิด"]
```

### Custom Dictionary

```csharp
using Thainlp;
using System.Collections.Generic;

// Create custom dictionary
var customWords = new List<string> { "ชินโซ", "อาเบะ" };
var customDict = new Trie(customWords);

// Use with tokenizer
var tokens = WordTokenizer.WordTokenize(
    "ชินโซ อาเบะ เกิด 21 กันยายน",
    customDict: customDict
);
```

### TCC (Thai Character Cluster) Tokenization

```csharp
using Thainlp;

// Tokenize into character clusters
var clusters = TCC.Segment("ประเทศไทย");
// Output: ["ป", "ระ", "เท", "ศ", "ไ", "ท", "ย"]

// Get cluster positions
var positions = TCC.GetPositions("ประเทศไทย");
```

### Legacy Subword API

```csharp
using Thainlp;

// Original TCC implementation
var clusters = Subword.tcc("ประเทศไทย");
var positions = Subword.tcc_pos("ประเทศไทย");
```

## API Compatibility with PyThaiNLP

This library provides an API similar to PyThaiNLP:

| PyThaiNLP | thainlp.net |
|-----------|-------------|
| `word_tokenize(text)` | `WordTokenizer.WordTokenize(text)` |
| `word_tokenize(text, engine="newmm")` | `WordTokenizer.WordTokenize(text, engine: "newmm")` |
| `word_tokenize(text, custom_dict=trie)` | `WordTokenizer.WordTokenize(text, customDict: trie)` |
| `word_tokenize(text, keep_whitespace=False)` | `WordTokenizer.WordTokenize(text, keepWhitespace: false)` |

## Testing

Run the test suite:
```bash
dotnet test
```

## Publishing to NuGet

The project is configured to automatically publish to NuGet when changes are pushed to the `main` branch.

To enable automatic publishing:
1. Create a NuGet API key at [nuget.org](https://www.nuget.org/account/apikeys)
2. Add the API key as a secret in your GitHub repository settings:
   - Go to Settings → Secrets and variables → Actions
   - Add a new repository secret named `NUGET_API_KEY`
   - Paste your NuGet API key as the value

The GitHub Actions workflow will automatically:
- Build the project
- Run tests
- Create the NuGet package
- Upload the package as an artifact
- Publish to NuGet (only on main branch)

## License

See LICENSE file for details.

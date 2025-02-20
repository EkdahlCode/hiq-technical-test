using Services.Interfaces;
using Shared.Dtos;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DocumentService : IDocumentService
    {
        public UploadDocumentResponse ProcessDocument(string documentContent)
        {
            var response = new UploadDocumentResponse();

            documentContent = ReplaceNonCharacters(documentContent);

            string[] splittedWords = SplitWords(documentContent);

            var wordFrequency = GroupWordFrequency(splittedWords);

            var words = GetMostFrequentWords(wordFrequency);

            response.Text = ReplaceWords(words, documentContent);

            return response;
        }

        internal string ReplaceWords(List<string> words, string documentContent)
        {
            foreach (var word in words)
                documentContent = documentContent.Replace($"{word}", $"foo{word}bar");

            return documentContent;
        }

        internal List<string> GetMostFrequentWords(List<WordDto> wordFrequency)
        {
            int maxWords = wordFrequency.Max(e => e.Count);

            var words = wordFrequency.Where(e => e.Count == maxWords)
                                     .Select(e => e.Text)
                                     .ToList();
            return words;
        }

        internal List<WordDto> GroupWordFrequency(string[] splittedWords)
        {
            return splittedWords.GroupBy(e => e, e => e.Count())
                                       .ToDictionary(e => e.Key, e => e.Count())
                                       .Select(g => new WordDto { Text = g.Key, Count = g.Value })
                                       .ToList();
        }

        internal string[] SplitWords(string documentContent)
        {
            return documentContent.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }

        internal string ReplaceNonCharacters(string documentContent)
        {
            documentContent = documentContent.Replace(".", string.Empty);
            documentContent = documentContent.Replace(",", string.Empty);
            documentContent = documentContent.Replace("?", string.Empty);
            documentContent = documentContent.Replace("-", string.Empty);

            return documentContent;
        }
    }
}

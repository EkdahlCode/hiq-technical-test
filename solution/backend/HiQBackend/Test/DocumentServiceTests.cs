using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class DocumentServiceTests
    {
        private DocumentService _documentService;

        public DocumentServiceTests()
        {
            _documentService = new DocumentService();
        }

        [Fact]
        public void AddFooBar()
        {
            string text = "HiQ";

            var result = _documentService.ProcessDocument(text);

            Assert.NotNull(result);
            Assert.Contains("foohiqbar", result.Text.ToLower());
        }

        [Fact]
        public void SplitWords()
        {
            string text = "HiQ Qih HQi";

            var result = _documentService.SplitWords(text);

            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void ReplaceNonCharacters()
        {
            string text = ".,-?";

            var result = _documentService.ReplaceNonCharacters(text);

            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.Response;
using System.Text;

namespace HiQBackend.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService) 
        { 
            _documentService = documentService;
        }

        [HttpPost()]
        [Route("upload")]
        public async Task<ActionResult<UploadDocumentResponse>> Upload([FromForm] IFormFile file)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string fileContent = Encoding.UTF8.GetString(fileBytes);

                if (string.IsNullOrWhiteSpace(fileContent))
                    return new UploadDocumentResponse() { Errror = "Filen innehåller inte text" };

                return this._documentService.ProcessDocument(fileContent);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

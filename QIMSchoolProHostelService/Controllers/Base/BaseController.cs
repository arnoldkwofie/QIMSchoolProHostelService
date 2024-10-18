using MediatR;
using Microsoft.AspNetCore.Mvc;
using Qface.Application.Shared.Common.Models;
using Here;

namespace QIMSchoolProHostelService.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


        protected IActionResult SmartResponse<TLeft, TRight>(Either<TLeft, TRight> either) where TLeft : DomainResponseBase
        {
            if (either.IsLeft)
                return StatusCode(either.LeftValue.Code, either.LeftValue);
            return Ok(either.RightValue);
        }
        protected async Task<FileResult> ReturnDocumentFileAsync(string filePath, string fileName)
        {
            var path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\Documents\\", fileName
                           );

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));

        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".csv", "text/csv"}
            };
        }

    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Files.Commands;
using Template.Application.Interfaces;
using Template.Domain.Files;

namespace Template.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var id = Guid.NewGuid();
            var path = Path.Combine("uploads", $"{id}_{file.FileName}");

            Directory.CreateDirectory("uploads");

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            var jobId = await _mediator.Send(
                new UploadFileCommand(path));

            return Accepted(new { jobId });
        }
    }
}

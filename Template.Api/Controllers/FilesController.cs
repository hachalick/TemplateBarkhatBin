using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Files.Commands;

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
            var path = Path.Combine("uploads", Guid.NewGuid() + file.FileName);

            using var stream = System.IO.File.Create(path);
            await file.CopyToAsync(stream);

            var jobId = await _mediator.Send(
                new UploadFileCommand(path));

            return Ok(new { jobId });
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Domain.Files;

namespace Template.Application.Files.Commands.UploadFile
{
    public class UploadFileCommandHandler
        : IRequestHandler<UploadFileCommand, Guid>
    {
        private readonly IFileJobRepository _repository;

        public UploadFileCommandHandler(IFileJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(
            UploadFileCommand request,
            CancellationToken ct)
        {
            var job = FileJob.Create(request.FilePath);

            await _repository.AddAsync(job);

            return job.Id;
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Files.Commands
{
    public record UploadFileCommand(string FilePath)
        : IRequest<Guid>;
}

using MediatR;
using Microsoft.AspNetCore.Http;

namespace Study_Project.Application.Features.Documents.Commands.UploadDocument
{
    public class UploadDocumentCommand : IRequest<bool>
    {
        public int EmployeeId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}

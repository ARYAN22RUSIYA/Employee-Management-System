using MediatR;
using Microsoft.Extensions.Configuration;
using Study_Project.Application.DTOs;
using Study_Project.Application.Features.Documents.Queries;

namespace Application.Features.Employee.Queries.GetEmployeeDocument
{
    public class GetEmployeeDocumentHandler : IRequestHandler<GetEmployeeDocumentQuery, FileDownloadDto>
    {
        private readonly JwtContext _context;
        private readonly IConfiguration _configuration;

        public GetEmployeeDocumentHandler(JwtContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<FileDownloadDto> Handle(GetEmployeeDocumentQuery request, CancellationToken cancellationToken)
        {
            var document = _context.Documents
                .FirstOrDefault(d => d.EmployeeId == request.EmployeeId && d.FileName == request.FileName);

            if (document == null || !File.Exists(document.FilePath))
                throw new FileNotFoundException("Document not found.");

            var fileBytes = await File.ReadAllBytesAsync(document.FilePath, cancellationToken);

            return new FileDownloadDto
            {
                FileContent = fileBytes,
                ContentType = document.ContentType,
                FileName = document.FileName
            };
        }
    }
}
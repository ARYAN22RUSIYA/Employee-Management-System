// GetEmployeeDocumentQuery.cs
using MediatR;
using Study_Project.Application.DTOs;

namespace Study_Project.Application.Features.Documents.Queries
{
    public class GetEmployeeDocumentQuery : IRequest<FileDownloadDto>
    {
        public int EmployeeId { get; set; }
        public string FileName { get; set; } // Optional: if multiple files per employee
    }
}

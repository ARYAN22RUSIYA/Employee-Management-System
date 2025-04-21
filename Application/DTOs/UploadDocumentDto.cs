using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Study_Project.Application.DTOs
{
    public class UploadDocumentDto
    {
        public int EmployeeId { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}

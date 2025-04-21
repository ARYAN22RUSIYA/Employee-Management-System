using Core.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Study_Project.Application.Features.Documents.Commands.UploadDocument;

public class UploadDocumentHandler : IRequestHandler<UploadDocumentCommand, bool>
{
    private readonly JwtContext _context;
    private readonly IConfiguration _configuration;

    public UploadDocumentHandler(JwtContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<bool> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FindAsync(request.EmployeeId);
        if (employee == null)
            return false;

        var uploadsFolder = _configuration["FileStorageSettings:Path"];
        if (string.IsNullOrWhiteSpace(uploadsFolder))
            throw new InvalidOperationException("File storage path is not configured.");

        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        var document = new Document
        {
            FileName = request.File.FileName,
            FilePath = filePath,
            ContentType = request.File.ContentType,
            EmployeeId = request.EmployeeId
        };

        _context.Documents.Add(document);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

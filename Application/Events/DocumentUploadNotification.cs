using MediatR;

namespace Application.Features.Documents.Events
{
    public class DocumentUploadedNotification : INotification
    {
        public int EmployeeId { get; }
        public string EmployeeEmail { get; }
        public string EmployeeName { get; }
        public string FileName { get; }

        public DocumentUploadedNotification(int employeeId, string employeeEmail, string employeeName, string fileName)
        {
            EmployeeId = employeeId;
            EmployeeEmail = employeeEmail;
            EmployeeName = employeeName;
            FileName = fileName;
        }
    }
}

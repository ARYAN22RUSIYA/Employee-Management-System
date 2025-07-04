using Core.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Events
{
    public class DocumentUploadedEmailHandler : INotificationHandler<DocumentUploadedNotification>
    {
        private readonly IBackgroundJobService _backgroundJobService;

        public DocumentUploadedEmailHandler(IBackgroundJobService backgroundJobService)
        {
            _backgroundJobService = backgroundJobService;
        }

        public Task Handle(DocumentUploadedNotification notification, CancellationToken cancellationToken)
        {
            _backgroundJobService.EnqueueSendEmail(
                notification.EmployeeEmail,
                "Document Uploaded",
                "d-14532afdb9154ae7ad7c4cef09ce2523 ", // Replace with your SendGrid template ID
                new { name = notification.EmployeeName, fileName = notification.FileName }
            );
            return Task.CompletedTask;
        }
    }
}

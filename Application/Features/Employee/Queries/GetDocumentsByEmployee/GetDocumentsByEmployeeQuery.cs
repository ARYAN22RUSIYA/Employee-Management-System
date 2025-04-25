using MediatR;
using Study_Project.Application.DTOs;

public class GetDocumentsByEmployeeQuery : IRequest<List<DocumentDto>>
{
    public int EmployeeId { get; set; }

    public GetDocumentsByEmployeeQuery(int employeeId)
    {
        EmployeeId = employeeId;
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Study_Project.Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

public class GetDocumentsByEmployeeHandler : IRequestHandler<GetDocumentsByEmployeeQuery, List<DocumentDto>>
{
    private readonly JwtContext _context;
    private readonly IMapper _mapper;

    public GetDocumentsByEmployeeHandler(JwtContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocumentDto>> Handle(GetDocumentsByEmployeeQuery request, CancellationToken cancellationToken)
    {
        return await _context.Documents
            .Where(d => d.EmployeeId == request.EmployeeId)
            .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

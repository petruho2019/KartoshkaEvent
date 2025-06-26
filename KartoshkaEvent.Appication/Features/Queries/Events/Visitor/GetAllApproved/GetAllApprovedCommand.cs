using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetAllApproved;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Events.Visitor.GetAllApproved
{
    public record GetAllApprovedCommand : IRequest<List<ApprovedEventDto>>;
}

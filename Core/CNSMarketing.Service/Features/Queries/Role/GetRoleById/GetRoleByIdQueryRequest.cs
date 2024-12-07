using MediatR;

namespace CNSMarketing.Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryRequest : IRequest<GetRoleByIdQueryResponse>
    {
        public required string Id { get; set; }
    }
}

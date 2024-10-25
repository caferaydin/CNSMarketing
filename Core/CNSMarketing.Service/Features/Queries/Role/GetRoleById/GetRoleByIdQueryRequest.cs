using MediatR;

namespace CNSMarketing.Service.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryRequest : IRequest<GetRoleByIdQueryResponse>
    {
        public required string Id { get; set; }
    }
}

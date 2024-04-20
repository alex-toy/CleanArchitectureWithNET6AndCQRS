using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Members.Queries.GetMemberById;

internal sealed class GetMemberByIdQueryHandler : IQueryHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberResponse>> Handle (GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository.GetByIdAsync (request.MemberId, cancellationToken);

        if (member is null)
        {
            Error error = new ("Member.NotFound", $"The member with Id {request.MemberId} was not found");
            return Result.Failure<MemberResponse>(error);
        }

        MemberResponse response = new (member.Id, member.Email.Value);

        return response;
    }
}
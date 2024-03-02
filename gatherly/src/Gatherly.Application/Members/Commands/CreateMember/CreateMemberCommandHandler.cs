﻿using Gatherly.Application.Members.Commands.CreateMember;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Gatherly.Domain.ValueObjects;
using MediatR;

namespace Gatherly.Application.Members.Commands.CreateGathering;

internal sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        if (firstNameResult.IsFailure) return Unit.Value;

        var member = new Member(Guid.NewGuid(), firstNameResult.Value, request.LastName, request.Email);
        _memberRepository.Add(member);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
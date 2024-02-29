﻿using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using MediatR;
using System.Xml.Linq;

namespace Gatherly.Application.Gatherings.Commands.CreateGathering;

internal sealed class CreateGatheringCommandHandler : IRequestHandler<CreateGatheringCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGatheringCommandHandler(
        IMemberRepository memberRepository,
        IGatheringRepository gatheringRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
    {
        Member? member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null) return Unit.Value;

        Gathering gathering = Gathering.Create(request.MemberId, member, request.Type, request.ScheduledAtUtc, request.Location, request.Name, request.MaximumNumberOfAttendees, request.InvitationsValidBeforeInHours);

        _gatheringRepository.Add(gathering);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
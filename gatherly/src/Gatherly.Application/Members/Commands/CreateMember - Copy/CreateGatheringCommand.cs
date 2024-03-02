using Gatherly.Domain.Enums;
using MediatR;

namespace Gatherly.Application.Gatherings.Commands.CreateGathering;

public sealed record CreateMemberCommand(
    Guid MemberId,
    GatheringType Type,
    DateTime ScheduledAtUtc,
    string Name,
    string? Location,
    int? MaximumNumberOfAttendees,
    int? InvitationsValidBeforeInHours) : IRequest;
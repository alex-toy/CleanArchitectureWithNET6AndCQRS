using Gatherly.Application.Abstractions.Messaging;

namespace Gatherly.Application.Members.Commands.CreateMember;

public sealed record CreateMemberCommand (string FirstName, string LastName, string Email) : ICommand<Guid>;
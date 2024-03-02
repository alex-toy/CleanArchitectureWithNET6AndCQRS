﻿using MediatR;

namespace Gatherly.Application.Members.Commands.CreateMember
{
    public sealed record CreateMemberCommand (string FirstName, string LastName, string Email) : IRequest;
}
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors;

public static class DomainErrors
{
    public static class Gathering
    {
        public static readonly Error InvitingCreator = new Error("Gathering.InvitingCreator", "Can't send invitation to the gathering creator.");
        public static readonly Error AlreadyPassed = new Error("Gathering.AlreadyPassed", "Can't send invitation for gathering in the past.");
    }
    public static class Member
    {
        public static readonly Error EmailAlreadyInUse = new Error("Member.EmailAlreadyInUse", "Email already in use.");
    }
}

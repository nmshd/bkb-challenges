using Enmeshed.BuildingBlocks.Application.Abstractions.Exceptions;

namespace Challenges.Application.Challenges
{
    public static class ApplicationErrors
    {
        public static ApplicationError ChallengeExpired()
        {
            return new ApplicationError("error.platform.challenges.challengeExpired", "The challenge has expired.");
        }
    }
}

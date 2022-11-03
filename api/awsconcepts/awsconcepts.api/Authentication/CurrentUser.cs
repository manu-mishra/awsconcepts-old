using Application.Common.Interfaces;

namespace awsconcepts.api.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        public string Id => "anonomous";
    }
}

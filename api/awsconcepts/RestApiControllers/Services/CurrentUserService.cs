﻿using Application.Common.Interfaces;

namespace RestApiControllers.Services
{
    internal class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            Id = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(predicate: x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
#pragma warning restore CS8601 // Possible null reference assignment.
            if (string.IsNullOrEmpty(Id))
            {
                Id = "Anonomous";
            }
        }
        public string Id { get; private set; }

        public Task<string> GetUseremail()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNickName()
        {
            throw new NotImplementedException();
        }
    }
}

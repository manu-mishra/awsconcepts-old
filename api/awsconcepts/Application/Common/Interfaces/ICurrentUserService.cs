﻿namespace Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        string Id { get; }
        Task<string> GetUserNickName();
        Task<string> GetUseremail();
    }
}

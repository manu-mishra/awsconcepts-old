﻿namespace Domain.Jobs
{
    public class Owner
    {
        public Owner(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

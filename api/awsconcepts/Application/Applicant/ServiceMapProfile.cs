﻿using AutoMapper;

namespace Application.Applicant
{
    public class ServiceMapProfile : Profile
    {
        public ServiceMapProfile()
        {
            CreateMap<Domain.Applicants.Profile, Dto.ApplicantProfile>();
            CreateMap<Domain.Applicants.ProfileDraft, Dto.ApplicantProfileDraft>();
            CreateMap<Domain.Applicants.Profile, Dto.ApplicantProfileSummary>();
            CreateMap<Domain.Applicants.ProfileDraft, Dto.ApplicantProfileSummary>();


            CreateMap<Dto.ApplicantProfile, Domain.Applicants.Profile>();
            CreateMap<Dto.ApplicantProfileDraft, Domain.Applicants.ProfileDraft>();
        }
    }
}

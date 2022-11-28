using AutoMapper;

namespace Application.Jobs
{
    public class ServiceMapProfile : Profile
    {
        public ServiceMapProfile()
        {
            CreateMap<Domain.Organizations.Job, Dto.Job>().ForAllMembers(x => x.AllowNull());


            CreateMap<Dto.Job, Domain.Organizations.Job>().ForAllMembers(x => x.AllowNull());
        }
    }
}
